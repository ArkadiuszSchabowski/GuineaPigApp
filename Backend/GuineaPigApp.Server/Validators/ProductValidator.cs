using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;

namespace GuineaPigApp.Server.Validators
{
    public class ProductValidator : IProductValidator
    {
        private readonly MyDbContext _context;

        public ProductValidator(MyDbContext context)
        {
            _context = context;
        }
        public void ValidateName(string name)
        {
            var product = _context.Products.SingleOrDefault(x => x.Name == name);

            if(product != null)
            {
                throw new ConflictException("Podany produkt istnieje już w bazie danych");
            }
        }
    }
}
