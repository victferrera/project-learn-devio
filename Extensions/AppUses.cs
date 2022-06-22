namespace AppMercadoBasico.Extensions
{
    public static class AppUses
    {
        public static WebApplication AddAppUses(this WebApplication uses)
        {
            uses.UseStaticFiles();
            uses.UseHttpsRedirection();

            return uses;
        }
    }
}
