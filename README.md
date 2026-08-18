# Nop.Plugin.Widgets.LimitedEdition

**nopCommerce widget for time-limited product offers**

Countdown timers, multi-template cards and cart popups, scarcity (stock / sold), progress bars, social proof, dynamic badges, and full admin customization.

| | |
|---|---|
| **System name** | `Widgets.LimitedEdition` |
| **Version** | BETA 0.7 |
| **nopCommerce** | 4.70 · 4.80 · 4.90 |
| **Author** | Limited Edition |

---

## Table of contents

1. [Features](#features)
2. [Requirements](#requirements)
3. [Installation](#installation)
4. [Upgrade](#upgrade)
5. [Quick start](#quick-start)
6. [Card templates](#card-templates)
7. [Popup templates](#popup-templates)
8. [Limited-edition products](#limited-edition-products)
9. [Scarcity, progress bar & badges](#scarcity-progress-bar--badges)
10. [Cart popup](#cart-popup)
11. [Social proof & other features](#social-proof--other-features)
12. [Widget zones](#widget-zones)
13. [Architecture](#architecture)
14. [Database](#database)
15. [Public APIs](#public-apis)
16. [Admin – what configures what](#admin--what-configures-what)
17. [Troubleshooting](#troubleshooting)
18. [Development](#development)
19. [Changelog](#changelog)
20. [Support](#support)

---

## Features

### Product / homepage card
- Countdown (days, hours, minutes, seconds) with customizable labels
- **5 independent card templates**: Classic, Minimal, Neon, Soft, Story
- Per-template colors, typography, padding, CTA, badge, and layout
- Timer layouts: horizontal, boxed, minimal
- Live admin preview
- Optional **product image as card background** (great with Story)
- Texts: badge, message, CTA, expired label

### Cart popup
- **4 independent popup templates**: Classic, Minimal, Neon, Soft
- Delay, overlay, blur, animations, ESC / overlay close
- List of still-valid limited products
- Optional once-per-session display

### Scarcity & urgency
- Initial / remaining / sold quantities (per product)
- Progress bar (time-based or stock-based)
- Dynamic badges (last hours, almost sold out, sold out)
- Optional purchase blocking when expired / sold out

### Social proof & extras
- Recent-activity toasts (purchases / views — real or simulated)
- Optional server-synced countdown
- Optional last-hour sound (Web Audio API)
- Optional template A/B testing
- Optional expiry reminder scheduled task
- Template preset export / import (JSON)

---

## Requirements

- **nopCommerce** 4.70, 4.80, or 4.90
- **.NET** matching your nopCommerce version
- **SQL Server** (tested on SQL Server 2022; other nopCommerce-supported databases may work)
- Admin permission: `Manage Widgets`

---

## Installation

### 1. Deploy files

Copy the full plugin folder to:

```text
{nopCommerce}/Plugins/Widgets.LimitedEdition/
```

Expected minimum layout:

```text
Plugins/Widgets.LimitedEdition/
├── Nop.Plugin.Widgets.LimitedEdition.dll
├── plugin.json
├── Views/
├── ...
```

> In development, build output usually goes to `Presentation/Nop.Web/Plugins/Widgets.LimitedEdition/` (see `.csproj`).

### 2. Restart

Restart the site (IIS / `dotnet run` / container).

### 3. Install from admin

1. Go to **Configuration → Local plugins**
2. Find **Limited Edition** (`Widgets.LimitedEdition`)
3. Click **Install**
4. Enable the plugin if it is not already active

Installation will:
- register default settings
- run migrations (`LimitedTimeProduct`, `CustomerTable`, `SocialProofEvent`, scarcity columns)
- load localization resources

### 4. Configure

**Configuration → Widgets** (or the plugin **Configure** link)  
Typical URL:

```text
/Admin/LimitedTime/Configure
```

---

## Upgrade

1. Replace plugin files with the new version
2. Bump version in `plugin.json` if needed
3. Restart the application
4. nopCommerce runs **Update** migrations

If scarcity columns are missing (SQL error `Invalid column name 'InitialQuantity'`…), run:

```text
Data/AddScarcityColumns.sql
```

against the nopCommerce database (SQL Server).

---

## Quick start

1. Open plugin **Configure** → pick a card template → set colors/texts → **Save**
2. In **Limited-time products** grid → add a product, set end date and optional quantities
3. Ensure the widget is enabled on homepage / product zones
4. Open homepage or product page and verify the countdown card

---

## Card templates

| Id | Name | Style |
|----|------|--------|
| 0 | **Classic** | Dark gold luxury, deep shadows |
| 1 | **Minimal** | Light, thin borders, low contrast |
| 2 | **Neon** | Dark with neon accent glow |
| 3 | **Soft** | Pastel, heavily rounded corners |
| 4 | **Story** | Full-bleed, fits product-image background |

### Customization

- Select a template from the admin dropdown
- Edit colors, fonts, sizes, badge, CTA, timer layout
- **Save** → changes apply to that template and the public view
- Switching template and saving also updates internal JSON presets

### Related options
- **Use product image as card background**
- **Prefer Story on homepage top**
- **A/B test templates** (optional per-session distribution)

---

## Popup templates

| Id | Name | Notes |
|----|------|--------|
| 0 | **Classic** | Dark gold, aligned with Classic card |
| 1 | **Minimal** | Light and simple |
| 2 | **Neon** | High contrast + glow |
| 3 | **Soft** | Pastel rounded |

Customizable: title, subtitle, continue text, delay, overlay opacity/blur, animation, max-width, badge, product list, glow/sheen.

---

## Limited-edition products

From the Configure grid:

| Field | Description |
|--------|-------------|
| Product | Catalog product autocomplete |
| End date/time | Offer expiry |
| Custom message | Overrides global message |
| **Initial qty** | Limited edition stock |
| **Remaining** | Units still available |
| **Sold** | Sales counter (updated on paid orders) |
| Show remaining | Scarcity text on card |
| Show sold | “already sold” text |
| Progress bar | Per-card progress |
| Progress mode | Time **or** stock |
| Discount % | Discount badge (e.g. `-15%`) |
| Block purchase when expired | Disables add-to-cart |

> Without **initial qty > 0**, the card will not show “N left”.  
> Progress bar can also be forced via the global **Progress bar default** flag.

---

## Scarcity, progress bar & badges

### Automatic stock updates
On `OrderPaidEvent`:
- decrements `RemainingQuantity` / increments `SoldCount`
- records a social-proof event

### Progress bar
- **Time**: remaining lifetime of the offer
- **Stock**: `sold / initial * 100`

### Dynamic badges (optional)
Examples:
- last hours before expiry
- almost sold out (configurable stock % threshold)
- SOLD OUT

---

## Cart popup

- Widget zone: `OrderSummaryContentBefore` (cart page)
- Typical conditions: popup enabled + valid limited products in context
- **Once per session**: does not show again in the same browser session
- Configurable delay (ms)

---

## Social proof & other features

| Feature | Description |
|---------|-------------|
| Social proof toasts | e.g. “Milan · purchased …” |
| Real events | From paid orders |
| Simulated events | Optional, for demo/fill |
| Server countdown | Endpoint realigns `data-end` and progress |
| Last-hour sound | Light beep via Web Audio if &lt; 1h left |
| Add-to-cart block | Consumer blocks cart when edition expired/sold out |
| Reminder task | Scheduled task (pre-expiry reminders) |
| Export/Import | JSON template presets from admin |

---

## Widget zones

| Zone | Usage |
|------|--------|
| `HomepageTop` | Offer cards at top of homepage |
| `HomepageBeforeProducts` | Cards before product listing |
| `ProductDetailsTop` | Compact card on product page |
| `OrderSummaryContentBefore` | Popup / content on cart |

Ensure your theme invokes these zones (`@await Component.InvokeAsync("Widget", new { widgetZone = "..." })`).

---

## Architecture

```text
Nop.Plugin.Widgets.LimitedEdition/
├── Components/
│   └── WidgetsLimitedEditionViewComponent.cs   # Zone routing → card / popup
├── Controllers/
│   ├── LimitedTimeController.cs                # Admin Configure, product CRUD, AJAX save
│   └── LimitedEditionPublicController.cs       # Countdown / SocialProof APIs
├── Consumers/
│   ├── OrderPaidConsumer.cs                    # Stock + social proof
│   ├── AddToCartBlockConsumer.cs               # Purchase block
│   └── EventConsumer.cs                        # Cart-related events
├── Data/
│   ├── LimitedTimeProductMigration.cs          # Install + Update schema
│   └── AddScarcityColumns.sql                  # Manual SQL Server script
├── Domain/
│   ├── LimitedTimeProduct.cs
│   ├── LimitedTimeSettings.cs
│   ├── CardTemplateType.cs / PopupTemplateType.cs
│   └── ...
├── Models/                                     # Admin + public models, StyleBag
├── Services/
│   ├── LimitedTimeProductService.cs
│   ├── LimitedEditionFeatureService.cs         # Enrich, scarcity, A/B, social
│   └── LimitedEditionReminderTask.cs
├── Views/
│   ├── Configure.cshtml                        # Admin shell + AJAX save
│   ├── _SettingsForm.cshtml                    # Card settings + live preview
│   ├── _PopupSettingsForm.cshtml
│   ├── _ProductsGrid.cshtml
│   └── Shared/Components/WidgetsLimitedEdition/
│       ├── LimitedEditionView.cshtml           # Public card
│       └── CartPopup.cshtml
├── plugin.json
└── README.md
```

### Style flow (admin → storefront)

1. Admin edits the form and clicks **Save**
2. `SaveSettingsAjax` writes **all** properties to `LimitedTimeSettings` (store 0 + current store)
3. Updates the JSON bag for the selected template (`CardTemplatesJson` / `PopupTemplatesJson`)
4. `WidgetsLimitedEditionViewComponent` loads settings and builds `StyleSettingsModel.FromLimitedTimeSettings`
5. Public view applies CSS variables and markup (badge, scarcity, progress, CTA)

### Multi-template
- Per-template presets stored as serialized JSON in settings
- Public view uses **flat settings** from the last save as source of truth
- Selected template IDs drive CSS classes (`le-tpl-classic`, `le-tpl-neon`, …)

---

## Database

### Tables

**LimitedTimeProduct**  
Limited product: `ProductId`, `EndDateUtc`, message, flags, scarcity columns (`InitialQuantity`, `RemainingQuantity`, `SoldCount`, `ShowRemainingStock`, `ShowSoldCount`, `ShowProgressBar`, `ProgressBarMode`, `DiscountPercentage`, `BlockPurchaseWhenExpired`).

**CustomerTable**  
Lightweight customer/cart tracking (internal).

**SocialProofEvent**  
Toast events: `ProductId`, `ProductName`, `EventType`, `CityOrRegion`, `CreatedOnUtc`.

### Settings
Stored in nopCommerce `Setting` table, e.g.:

```text
LimitedTimeSettings.AccentColor
LimitedTimeSettings.CardTemplatesJson
...
```

---

## Public APIs

Controller: `LimitedEditionPublic` (public area).

| Endpoint | Description |
|----------|-------------|
| `GET /LimitedEditionPublic/Countdown?productId=` | JSON with `endDateUtc`, progress, status |
| `GET /LimitedEditionPublic/SocialProofFeed?take=` | Event list for toasts |

> Enable *Server countdown* / *Social proof* from admin feature flags.

---

## Admin – what configures what

| UI area | Content |
|---------|---------|
| Card template + style form | Colors, texts, layout, typography, CTA, live preview |
| Advanced features | Social proof, dynamic badges, default progress, A/B, Story, image background, block purchase |
| Popup section | Popup template, delay, overlay, texts, animations |
| Products grid | Limited product CRUD, stock, dates, per-product flags |
| **Save** button | AJAX → saves **all** global settings + alert feedback |

---

## Troubleshooting

### “Save does nothing / no message”
- Use the updated Configure page (AJAX save with alert)
- Copy `Views/Configure.cshtml` and `Controllers/LimitedTimeController.cs`, rebuild, restart
- Check F12 → Network for `SaveSettingsAjax`

### “Save works but homepage does not change”
- Hard refresh (`Ctrl+F5`)
- Ensure `LimitedEditionView.cshtml` is the new file in the plugin folder
- Disable **A/B test** and **Prefer Story** if unused
- Clear nopCommerce cache and browser cache

### “No remaining stock / progress on card”
- On the limited product: set **Initial qty** and show stock/progress flags
- Or enable global **Progress bar default**
- Run scarcity migration/SQL if columns are missing

### SQL error `Invalid column name 'InitialQuantity'`
Run `Data/AddScarcityColumns.sql` on the database.

### Razor error `keyframes does not exist`
In CSS inside `.cshtml` use `@@keyframes` (not `@keyframes`).

### Popup does not appear
- `EnableCartPopup` is on
- Theme includes `OrderSummaryContentBefore`
- Limited products are still valid
- If *Once per session* is on, try a new session/browser

### Widget missing on homepage
- Plugin installed and **enabled**
- Widget active under **Configuration → Widgets**
- Theme invokes the correct widget zone

---

## Development

### Build

```bash
dotnet build Nop.Plugin.Widgets.LimitedEdition.csproj
```

Typical output path:

```text
Presentation/Nop.Web/Plugins/Widgets.LimitedEdition/
```

### Main dependencies
- `Nop.Web.Framework`
- `Nop.Services` / `Nop.Core` / `Nop.Data`
- FluentMigrator (plugin migrations)

### DI registration
`DependencyInjection/NopStartup.cs` — services, consumers, tasks.

### Conventions
- Settings: `ISettings` → `LimitedTimeSettings`
- Admin model: `ConfigurationModel` (nopCommerce record)
- Runtime style: `StyleSettingsModel.FromLimitedTimeSettings`
- Template presets: `StyleBag` + JSON dictionary

### Suggested manual tests
1. Save badge/color → reload admin → values persist  
2. Homepage shows the same colors  
3. Product with qty → scarcity text + progress  
4. Paid order → sold count updates  
5. Cart popup with a different template  
6. SQL script is idempotent on an already-migrated DB  

---

## Changelog

### BETA 0.6
- Multi-template cards (5) and popups (4) with independent presets
- Scarcity (qty / sold), progress bar, dynamic badges
- Social proof, server countdown, last-hour sound
- Product image as card background option
- Full admin AJAX save with user feedback
- Update migration + SQL script for scarcity columns
- `OrderPaid` consumer / add-to-cart blocking
- Public view aligned with admin settings (flat settings as source of truth)
- Documentation refresh

### BETA 0.5
- Advanced card and popup customization
- Live admin preview for card and popup
- Countdown widget on multiple public zones
- Improved settings model and configuration UI

### BETA 0.1
- Initial beta release
- Limited-time products with end date
- Basic countdown card on widget zones
- Admin product grid and base settings

---

## Support

- If needed contact me: nikozarro@gmail.com  or  ilpuntonico@gmail.com
- Use the GitHub issue tracker for bugs and feature requests
- When reporting issues, include: nopCommerce version, plugin version (e.g. BETA 0.6), error message, and whether scarcity SQL was applied
