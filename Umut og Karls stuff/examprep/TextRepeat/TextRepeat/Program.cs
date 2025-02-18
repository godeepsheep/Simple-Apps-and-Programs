// 1. Opretter en builder for webapplikationen
var builder = WebApplication.CreateBuilder(args);

// 2. Tilføjer de nødvendige services for controller og views til applikationen
builder.Services.AddControllersWithViews();

// 3. Bygger webapplikationen efter services er tilføjet
var app = builder.Build();

// 4. Middleware der automatisk omdirigerer HTTP-anmodninger til HTTPS
app.UseHttpsRedirection();

// 5. Middleware der aktiverer routing, som bestemmer hvilken controller og handling der skal håndtere anmodninger
app.UseRouting();

// 6. Middleware der aktiverer autorisation (håndterer brugerrettigheder)
app.UseAuthorization();

// 7. Middleware til at håndtere statiske filer (kan være tilpasset i applikationen)
app.MapStaticAssets();

// 8. Definerer en standard routing-konfiguration for controllers
app.MapControllerRoute(
        name: "default",               // Navnet på ruten
        pattern: "{controller=Home}/{action=Index}/{id?}") // URL-strukturen (f.eks. /Home/Index/123)
    .WithStaticAssets(); // Tilføjer statiske filer til ruten (kan være en tilpasset metode)

// 9. Starter applikationen og begynder at lytte på indkommende anmodninger
app.Run();