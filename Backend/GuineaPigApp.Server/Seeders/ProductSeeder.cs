using AutoMapper;
using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;

namespace GuineaPigApp.Server.Seeders
{
    public class ProductSeeder : IProductSeeder
    {
        private readonly IProductSeederRepository _repository;
        private readonly IMapper _mapper;
        private readonly MyDbContext _context;

        public ProductSeeder(IProductSeederRepository repository, IMapper mapper, MyDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }
        public void SeedData()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Products.Any())
                {
                    List<ProductDto> badProductsDto = GetBadProducts();
                    List<ProductDto> goodProductsDto = GetGoodProducts();

                    List<Product> badProducts = GetProducts(badProductsDto);
                    List<Product> goodProducts = GetProducts(goodProductsDto);

                    _repository.AddListProduct(badProducts);
                    _repository.AddListProduct(goodProducts);
                }
            }
        }
        public List<Product> GetProducts(List<ProductDto> dto)
        {
            List<Product> products = _mapper.Map<List<Product>>(dto);
            return products;
        }
        public List<ProductDto> GetBadProducts()
        {
            List<ProductDto> products = new List<ProductDto>(){
                new ProductDto()
                {
                    Name = "Czosnek",
                    Description = "Czosnek jest szkodliwy dla świnek morskich i dlatego jest całkowicie zakazany w ich diecie. Jego ostre właściwości mogą powodować drażnienie przewodu pokarmowego oraz zakłócenia w naturalnym trawieniu. Spożycie czosnku może prowadzić do poważnych problemów zdrowotnych, a nawet śmierci świnek morskich.",
                    ImageUrl = "/assets/images/badProducts/garlic.jpg",
                    IsGoodProduct = false
                },
                new ProductDto()
                {
                    Name = "Cebula",
                    Description = "Cebula jest szkodliwa dla świnek morskich i nie powinna być podawana w ich diecie. Zawiera substancje chemiczne, które mogą powodować uszkodzenie czerwonych krwinek oraz prowadzić do anemii. Spożycie cebuli może także drażnić przewód pokarmowy i prowadzić do problemów zdrowotnych, co stanowi poważne zagrożenie dla zdrowia świnek morskich.",
                    ImageUrl = "/assets/images/badProducts/onion.jpg",
                    IsGoodProduct = false
                },
                new ProductDto()
                {
                    Name = "Por",
                    Description = "Por, podobnie jak cebula i czosnek, zawiera substancje chemiczne, które są toksyczne dla świnek morskich. Spożycie pora może prowadzić do uszkodzenia czerwonych krwinek, anemii oraz poważnych problemów trawiennych.",
                    ImageUrl = "/assets/images/badProducts/leek.jpg",
                    IsGoodProduct = false
                },
                new ProductDto()
                {
                    Name = "Trawa - mokra",
                    Description = "Mokra trawa nie jest odpowiednia dla świnek morskich ze względu na wysokie ryzyko wystąpienia problemów zdrowotnych. Spożycie mokrej trawy może prowadzić do zaburzeń trawiennych oraz poważnych problemów żołądkowych, co jest szczególnie niebezpieczne dla delikatnego układu pokarmowego świnek morskich.",
                    ImageUrl = "/assets/images/badProducts/wet_grass.jpg",
                    IsGoodProduct = false
                },
                new ProductDto()
                {
                    Name = "Chleb",
                    Description = "Chleb, zwłaszcza nie całkowicie wysuszony, jest trudny do strawienia dla świnek morskich i może prowadzić do problemów trawiennych oraz wzdęć. Dodatkowo, chleb zawiera dużo węglowodanów, które mogą przyczyniać się do otyłości.",
                    ImageUrl = "/assets/images/badProducts/bread.jpg",
                    IsGoodProduct = false
                },
                new ProductDto()
                {
                    Name = "Sałata lodowa",
                    Description = "Sałata lodowa zawiera mało wartości odżywczych i może powodować biegunki u świnek morskich. Spożycie sałaty lodowej nie jest zalecane ze względu na potencjalne problemy zdrowotne.",
                    ImageUrl = "/assets/images/badProducts/iceberg_lettuce.jpg",
                    IsGoodProduct = false
                },
                new ProductDto()
                {
                    Name = "Sałata masłowa",
                    Description = "Sałata masłowa może powodować problemy trawienne u świnek morskich, takie jak biegunka. Chociaż zawiera więcej składników odżywczych niż sałata lodowa, nadal nie jest zalecana jako stały element diety świnek morskich.",
                    ImageUrl = "/assets/images/badProducts/butter_lettuce.jpg",
                    IsGoodProduct = false
                },
                new ProductDto()
                {
                    Name = "Grzyby",
                    Description = "Grzyby mogą zawierać toksyny, które są szkodliwe dla świnek morskich. Spożycie grzybów może prowadzić do poważnych problemów zdrowotnych, takich jak zaburzenia trawienne, zatrucia i uszkodzenie wątroby.",
                    ImageUrl = "/assets/images/badProducts/mushrooms.jpg",
                    IsGoodProduct = false
                },
                new ProductDto()
                {
                    Name = "Fasola",
                    Description = "Fasola zawiera lektiny, które są toksyczne dla świnek morskich. Spożycie fasoli może prowadzić do wzdęć, bólu brzucha i problemów trawiennych. Wszystkie rodzaje fasoli są niebezpieczne dla świnek morskich, szczególnie gdy są surowe.",
                    ImageUrl = "/assets/images/badProducts/beans.jpg",
                    IsGoodProduct = false
                },
                new ProductDto()
                {
                    Name = "Groch",
                    Description = "Groch zawiera wysoki poziom białka i skrobi, które mogą być trudne do strawienia przez świnki morskie. Spożycie grochu może prowadzić do wzdęć i problemów trawiennych.",
                    ImageUrl = "/assets/images/badProducts/peas.jpg",
                    IsGoodProduct = false
                },
                new ProductDto()
                {
                    Name = "Soja",
                    Description = "Soja zawiera fitoestrogeny i wysoką ilość białka, które mogą zakłócać procesy metaboliczne u świnek morskich. Spożycie soi może prowadzić do problemów trawiennych i hormonalnych.",
                    ImageUrl = "/assets/images/badProducts/soy.jpg",
                    IsGoodProduct = false
                },
                new ProductDto()
                {
                    Name = "Kolby dla gryzoni",
                    Description = "Kolby dla gryzoni często zawierają duże ilości cukru i sztucznych dodatków, które są szkodliwe dla świnek morskich. Świnki morskie powinny jeść tylko suchą trawę i siano, wybrane warzywa oraz niektóre owoce. Przetworzona żywność, taka jak kolby, może prowadzić do problemów zdrowotnych, takich jak otyłość i problemy trawienne.",
                    ImageUrl = "/assets/images/badProducts/rodent_treat_sticks.jpg",
                    IsGoodProduct = false
                }
            };

            return products;
        }
        public List<ProductDto> GetGoodProducts()
        {
            List<ProductDto> products = new List<ProductDto>()
        {
        new ProductDto()
        {
            Name = "Buraki",
            Description = "Buraki są bogate w witaminy i minerały, które są korzystne dla świnek morskich. Zawierają witaminę C, która jest niezbędna dla zdrowia tych zwierząt.",
            ImageUrl = "/assets/images/goodProducts/beetroot.jpg",
            IsGoodProduct = true
        },
        new ProductDto()
        {
            Name = "Marchew",
            Description = "Marchew jest doskonałym źródłem witaminy A i innych składników odżywczych. Świnki morskie uwielbiają jej smak, ale należy podawać ją w umiarkowanych ilościach ze względu na zawartość cukru.",
            ImageUrl = "/assets/images/goodProducts/carrot.jpg",
            IsGoodProduct = true
        },
        new ProductDto()
        {
            Name = "Pietruszka",
            Description = "Pietruszka jest bogata w witaminę C i inne składniki odżywcze. Jest to doskonała przekąska dla świnek morskich, która wspomaga ich zdrowie.",
            ImageUrl = "/assets/images/goodProducts/parsley_root.jpg",
            IsGoodProduct = true
        },
        new ProductDto()
        {
            Name = "Sałata rzymska",
            Description = "Sałata rzymska jest niskokaloryczna i zawiera dużo błonnika, co jest korzystne dla układu trawiennego świnek morskich. Zawiera również witaminy A i C.",
            ImageUrl = "/assets/images/goodProducts/romaine_lettuce.jpg",
            IsGoodProduct = true
        },
        new ProductDto()
        {
            Name = "Seler",
            Description = "Seler jest niskokaloryczny i zawiera witaminy oraz minerały, które są korzystne dla zdrowia świnek morskich. Jest również dobry dla ich zębów.",
            ImageUrl = "/assets/images/goodProducts/celery.jpg",
            IsGoodProduct = true
        },
        new ProductDto()
        {
            Name = "Pomidor",
            Description = "Pomidory są źródłem witaminy C i innych składników odżywczych. Świnki morskie mogą jeść pomidory, ale należy ograniczyć ilość z powodu zawartości kwasu.",
            ImageUrl = "/assets/images/goodProducts/tomato.jpg",
            IsGoodProduct = true
        },
        new ProductDto()
        {
            Name = "Jabłko",
            Description = "Jabłka są dobrą przekąską dla świnek morskich, ponieważ są bogate w błonnik i witaminy. Świnki morskie powinny jeść je w umiarkowanych ilościach ze względu na zawartość cukru.",
            ImageUrl = "/assets/images/goodProducts/apple.jpg",
            IsGoodProduct = true
        },
        new ProductDto()
        {
            Name = "Gruszka",
            Description = "Gruszki są niskokaloryczne i zawierają witaminy oraz błonnik, które są korzystne dla świnek morskich. Podawaj je w umiarkowanych ilościach.",
            ImageUrl = "/assets/images/goodProducts/pear.jpg",
            IsGoodProduct = true
        },
        new ProductDto()
        {
            Name = "Karma dla świnek",
            Description = "Karma Versele-Laga jest specjalnie opracowana dla świnek morskich, aby dostarczyć im wszystkich niezbędnych składników odżywczych, w tym witaminy C.",
            ImageUrl = "/assets/images/goodProducts/guinea_pig_food.jpg",
            IsGoodProduct = true
        },
        new ProductDto()
        {
            Name = "Ogórek zielony",
            Description = "Ogórki są niskokaloryczne i bogate w wodę, co jest korzystne dla nawodnienia świnek morskich.",
            ImageUrl = "/assets/images/goodProducts/cucumber.jpg",
            IsGoodProduct = true
        },
        new ProductDto()
        {
            Name = "Koperek",
            Description = "Koperek jest bezpiecznym ziołem dla świnek morskich, które można podawać w niewielkich ilościach. Zawiera witaminę C i dodaje smaku do ich diety.",
            ImageUrl = "/assets/images/goodProducts/dill.jpg",
            IsGoodProduct = true
        },
        new ProductDto()
        {
            Name = "Natka pietruszki",
            Description = "Natka pietruszki jest bogata w witaminę C i inne składniki odżywcze. Jest bezpieczna dla świnek morskich i może być dodatkiem do ich codziennej diety.",
            ImageUrl = "/assets/images/goodProducts/parsley.jpg",
            IsGoodProduct = true
        },
        new ProductDto()
        {
            Name = "Trawa - sucha",
            Description = "Trawa sucha jest istotnym elementem diety świnek morskich z kilku kluczowych powodów. Po pierwsze, jest ona bogatym źródłem błonnika, który wspiera zdrowie ich układu trawiennego poprzez regulację pracy jelit. Dodatkowo, trawa sucha odzwierciedla naturalną dietę świnek morskich, które w naturze spożywają różnorodne rośliny i trawy. Żucie trawy suchej nie tylko dostarcza im odpowiedniej ilości błonnika, ale również pomaga w ścieraniu zębów, co jest istotne dla zdrowia jamy ustnej tych zwierząt. Trawa zawiera również niezbędne witaminy i minerały, takie jak witamina C, która jest kluczowa dla świnek morskich, ponieważ nie mogą jej samodzielnie syntetyzować. Wreszcie, trawa sucha jest bezpiecznym źródłem pożywienia, o ile jest czysta i wolna od zanieczyszczeń. Dlatego regularne dostarczanie trawy suchej jest zalecane jako element zdrowej i zrównoważonej diety dla świnek morskich.",
            ImageUrl = "/assets/images/goodProducts/grass.jpg",
            IsGoodProduct = true
        }
            };
            return products;
        }
    }
}
