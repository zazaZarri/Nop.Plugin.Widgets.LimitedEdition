# Limited Edition – Feature complete

## Template
- Card: Classic, Minimal, Neon, Soft, **Story** (full-bleed con immagine prodotto)
- Popup: Classic, Minimal, Neon, Soft
- Ogni template ha stili JSON indipendenti; salvataggio aggiorna solo il selezionato
- Export / Import JSON da admin
- A/B test: assegna template per sessione (`EnableAbTest` + csv id)

## Scarsità
- `InitialQuantity` / `RemainingQuantity` / `SoldCount` sull’entità prodotto limited
- Decremento automatico a ordine pagato (`OrderPaidConsumer`)
- UI: “Restano N pezzi”, “N già venduti”, badge sconto %

## Progress bar
- Mode tempo o stock (per prodotto o default globale)

## Badge dinamici
- ULTIMA ORA, ULTIME XH, QUASI ESAURITO, N GIÀ VENDUTI, NUOVO DROP, ESAURITO/SOLD OUT

## Social proof
- Toast “Milano · ha acquistato …”
- Feed da eventi reali + opzione simulati
- API: `/LimitedEditionPublic/SocialProofFeed`

## Countdown server-side
- API: `/LimitedEditionPublic/Countdown?productId=`
- La view riallinea `data-end` e progress

## Blocco acquisto
- Flag per prodotto + globale
- CTA disabilitata + disable add-to-cart su product page
- API: `/LimitedEditionPublic/CanPurchase?productId=`

## Zone / layout
- Compatto su product page
- Prefer Story su HomepageTop

## Suono ultima ora
- Beep Web Audio ogni 60s se manca < 1h (opzionale)

## Reminder scadenza
- `LimitedEditionReminderTask` (IScheduleTask): log clienti con limited in carrello in scadenza
- Abilitare da Schedule tasks e collegare email custom se serve

## Preview admin
- Toggle desktop / mobile sulla live preview
- Switch template con reload stile via AJAX

## Multi-store
- Settings caricate/salvate per `store.Id` (template JSON inclusi)

## Sconto
- `DiscountPercentage` mostrato in badge sulla card (informativo)
- Per applicare sconto reale: creare regola Discount nopCommerce allineata alle date, oppure estendere con `IPriceCalculationService`
