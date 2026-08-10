using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.LimitedEdition.Domain;
using Nop.Plugin.Widgets.LimitedEdition.Models;
using Nop.Plugin.Widgets.LimitedEdition.Services;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.LimitedEdition.Controllers
{
    [Area(AreaNames.ADMIN)]
    [AuthorizeAdmin]
    [AutoValidateAntiforgeryToken]
    public class LimitedTimeController : BasePluginController
    {
        private readonly ILimitedTimeProductService _limitedTimeProductService;
        private readonly IProductService _productService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IPermissionService _permissionService;

        public LimitedTimeController(
            ILimitedTimeProductService limitedTimeProductService,
            IProductService productService,
            ISettingService settingService,
            IStoreContext storeContext,
            IPermissionService permissionService)
        {
            _limitedTimeProductService = limitedTimeProductService;
            _productService = productService;
            _settingService = settingService;
            _storeContext = storeContext;
            _permissionService = permissionService;
        }

        #region Configure

        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return AccessDeniedView();

            var store = await _storeContext.GetCurrentStoreAsync();
            var settings = await _settingService.LoadSettingAsync<LimitedTimeSettings>(store.Id);

            var model = new ConfigurationModel
            {
                BackgroundColor = settings.BackgroundColor,
                TextColor = settings.TextColor,
                CustomMessage = settings.CustomMessage,
                TimerLayoutId = (int)settings.TimerLayout,
                HideProductWhenExpired = settings.HideProductWhenExpired,
                ActiveStoreScopeConfiguration = store.Id,
                LimitedTimeProductSearchModel = new LimitedTimeProductSearchModel()
            };

            model.LimitedTimeProductSearchModel.SetGridPageSize();

            return View("~/Plugins/Widgets.LimitedEdition/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return AccessDeniedView();

            var store = await _storeContext.GetCurrentStoreAsync();
            var settings = await _settingService.LoadSettingAsync<LimitedTimeSettings>(store.Id);

            settings.BackgroundColor = model.BackgroundColor;
            settings.TextColor = model.TextColor;
            settings.CustomMessage = model.CustomMessage;
            settings.TimerLayout = (TimerLayoutType)model.TimerLayoutId;
            settings.HideProductWhenExpired = model.HideProductWhenExpired;

            await _settingService.SaveSettingAsync(settings, store.Id);
            await _settingService.ClearCacheAsync();

            return await Configure();
        }

        #endregion

        #region Products Grid

        [HttpPost]
        public async Task<IActionResult> ProductList(LimitedTimeProductSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return Json(new { Data = new List<LimitedTimeProductModel>(), Total = 0 });

            var items = await _limitedTimeProductService.GetAllPagedAsync(
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            var productModels = new List<LimitedTimeProductModel>();

            foreach (var item in items)
            {
                var product = await _productService.GetProductByIdAsync(item.ProductId);

                productModels.Add(new LimitedTimeProductModel
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    ProductName = product?.Name ?? $"[Prodotto eliminato - ID {item.ProductId}]",
                    StartDateUtc = item.StartDateUtc,
                    EndDateUtc = item.EndDateUtc,
                    IsActive = item.IsActive
                });
            }

            return Json(new
            {
                Data = productModels,
                Total = items.TotalCount
            });
        }

        [HttpPost]
        public async Task<IActionResult> ProductUpdate(LimitedTimeProductModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return Json(new { Result = false, Error = "Access denied" });

            if (model.Id > 0)
            {
                var entity = await _limitedTimeProductService.GetByIdAsync(model.Id);
                if (entity == null)
                    return Json(new { Result = false, Error = "Record non trovato" });

                entity.ProductId = model.ProductId;
                entity.StartDateUtc = model.StartDateUtc ?? DateTime.UtcNow;
                entity.EndDateUtc = model.EndDateUtc ?? DateTime.UtcNow.AddDays(7);
                entity.IsActive = model.IsActive;

                await _limitedTimeProductService.UpdateAsync(entity);
            }
            else
            {
                var entity = new LimitedTimeProduct
                {
                    ProductId = model.ProductId,
                    StartDateUtc = model.StartDateUtc ?? DateTime.UtcNow,
                    EndDateUtc = model.EndDateUtc ?? DateTime.UtcNow.AddDays(7),
                    IsActive = model.IsActive
                };

                await _limitedTimeProductService.InsertAsync(entity);
            }

            return Json(new { Result = true });
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return Json(new { Result = false, Error = "Access denied" });

            var entity = await _limitedTimeProductService.GetByIdAsync(id);
            if (entity == null)
                return Json(new { Result = false, Error = "Record non trovato" });

            await _limitedTimeProductService.DeleteAsync(entity);

            return Json(new { Result = true });
        }

        [HttpGet]
        public async Task<IActionResult> ProductSearchAutocomplete(string term)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_WIDGETS))
                return Json(new List<object>());

            if (string.IsNullOrWhiteSpace(term))
                return Json(new List<object>());

            term = term.Trim();
            var result = new List<object>();

            // Cerca per ID se è un numero
            if (int.TryParse(term, out int productId))
            {
                var product = await _productService.GetProductByIdAsync(productId);
                if (product != null)
                {
                    result.Add(new
                    {
                        id = product.Id,
                        label = $"{product.Name} [ID: {product.Id}]",
                        value = product.Name
                    });
                    return Json(result); 
                }
            }
            
            var products = await _productService.SearchProductsAsync(
                pageIndex: 0,
                pageSize: 25,
                keywords: term,
                searchSku: true,
                searchManufacturerPartNumber: true,
                showHidden: true,          
                visibleIndividuallyOnly: false
            );

            foreach (var p in products)
            {
                result.Add(new
                {
                    id = p.Id,
                    label = $"{p.Name} [ID: {p.Id}]",
                    value = p.Name
                });
            }

            return Json(result);
        }

        #endregion
    }
}