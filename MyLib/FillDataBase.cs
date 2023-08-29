using SPF.Data;
using SPF.Models.Items;

namespace SPF.MyLibrary
{
    public class FillDataBase
    {
        static private ApplicationDbContext _context;

        public FillDataBase(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Fill()
        {
            FillGroup();
            FillCategory();
            FillCategoryGroup();
            FillType();
            FillTypeCategory();
            Action();
            CreateChainOfItemAndSubType();
        }
        public void Action()
        {
            var rand = new Random();
            FillItem("Костюм", "None", "Костюм универсал", (decimal)99.14, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Халат", "None", "Халат обычный", (decimal)14, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Трикотаж", "None", "Ммм гуд", (decimal)16.12, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Куртка", "None", "Куртка клепаная", (decimal)912.1, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Жилет", "None", "Жилет/разгрузка", (decimal)12.43, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Брюки", "None", "Брюки обычные", (decimal)93.3, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Полукомбинезоны", "None", "Полукомбинезоны", (decimal)16.73, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Костюмы сварщика утепленные", "None", "Костюмы сварщика утепленные усиленный", (decimal)99.14, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Ботинки", "None", "Ботинки защитные", (decimal)76.36, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Сапоги", "None", "Сапоги резиновые", (decimal)43.1, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Тапочки", "None", "Тапочки пляжные", (decimal)64.23, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Обувь войлочная", "None", "Обувь войлочная зимняя", (decimal)99.14, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Галоши", "None", "Галоши резиновые", (decimal)54.31, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Зрения", "None", "Очки, а не очки", (decimal)75.00, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Дыхания", "None", "Маска медицинская", (decimal)25.25, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Перчатки", "None", "Перчатки обыкновенные", (decimal)99.14, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Рукавицы", "None", "Рукавицы с доп. изоляцией", (decimal)99.14, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Краги", "None", "Краги, как Краги", (decimal)99.14, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Головы", "None", "Шлем универсальный", (decimal)99.14, $"{rand.Next()}", $"{rand.Next()}", "None");
            FillItem("Сигнальные жилеты", "None", "Сигнальные жилеты красного цвета", (decimal)99.14, $"{rand.Next()}", $"{rand.Next()}", "None");
        }
        public void ClearAllTables()
        {
            _context.RemoveRange(_context.Groups);
            _context.RemoveRange(_context.Categories);
            _context.RemoveRange(_context.Types);
            _context.RemoveRange(_context.Items);
            _context.RemoveRange(_context.CategoryGroups);
            _context.RemoveRange(_context.TypeCategories);
            _context.RemoveRange(_context.ItemTypes);
            _context.RemoveRange(_context.ItemsSpecification);
            _context.SaveChanges();
        }
        private void FillGroup()
        {
            _context.Groups.AddRange(new Group() { Name = "Спецодежда" },
                                     new Group() { Name = "Обувь" },
                                     new Group() { Name = "СИЗ" },
                                     new Group() { Name = "Прочее" });
            _context.SaveChanges();
        }

        private void FillCategoryGroup()
        {
            var clothesId = _context.Groups.Where(o => o.Name == "Спецодежда").FirstOrDefault().Id;
            var shoesId = _context.Groups.Where(o => o.Name == "Обувь").FirstOrDefault().Id;
            var PPEId = _context.Groups.Where(o => o.Name == "СИЗ").FirstOrDefault().Id;
            var otherId = _context.Groups.Where(o => o.Name == "Прочее").FirstOrDefault().Id;

            var summerSuitId = _context.Categories.Where(o => o.Name == "Летняя Спецодежда").FirstOrDefault().Id;
            var summerShoesId = _context.Categories.Where(o => o.Name == "Летняя Обувь").FirstOrDefault().Id;
            var winterSuitId = _context.Categories.Where(o => o.Name == "Зимняя Спецодежда").FirstOrDefault().Id;
            var winterShoesId = _context.Categories.Where(o => o.Name == "Зимняя Обувь").FirstOrDefault().Id;
            var protectiveId = _context.Categories.Where(o => o.Name == "Защитная").FirstOrDefault().Id;
            var PVCandEVAId = _context.Categories.Where(o => o.Name == "ПВХ ЭВА").FirstOrDefault().Id;
            var PersonalProtectiveEquipmentId = _context.Categories.Where(o => o.Name == "Средства Индивидуальной Защиты").FirstOrDefault().Id;
            var HandProtectiveEquipmentId = _context.Categories.Where(o => o.Name == "Средства Защиты Рук").FirstOrDefault().Id;

            _context.CategoryGroups.AddRange(new CategoryGroup() { CategoryId = summerSuitId, GroupId = clothesId },
                                             new CategoryGroup() { CategoryId = summerShoesId, GroupId = shoesId },
                                             new CategoryGroup() { CategoryId = winterSuitId, GroupId = clothesId },
                                             new CategoryGroup() { CategoryId = winterShoesId, GroupId = shoesId },
                                             new CategoryGroup() { CategoryId = protectiveId, GroupId = clothesId },
                                             new CategoryGroup() { CategoryId = PVCandEVAId, GroupId = shoesId },
                                             new CategoryGroup() { CategoryId = PersonalProtectiveEquipmentId, GroupId = PPEId },
                                             new CategoryGroup() { CategoryId = HandProtectiveEquipmentId, GroupId = PPEId }
                                             );
            _context.SaveChanges();
        }

        private void FillCategory()
        {
            _context.Categories.AddRange(new Category() { Name = "Летняя Спецодежда" },
                                         new Category() { Name = "Зимняя Спецодежда" },
                                         new Category() { Name = "Летняя Обувь" },
                                         new Category() { Name = "Зимняя Обувь" },
                                         new Category() { Name = "Защитная" },
                                         new Category() { Name = "ПВХ ЭВА" },
                                         new Category() { Name = "Средства Индивидуальной Защиты" },
                                         new Category() { Name = "Средства Защиты Рук" });
            _context.SaveChanges();
        }

        private void FillTypeCategory()
        {
            var summerSuitId = _context.Categories.Where(o => o.Name == "Летняя Спецодежда").FirstOrDefault().Id;
            var summerShoesId = _context.Categories.Where(o => o.Name == "Летняя Обувь").FirstOrDefault().Id;
            var winterSuitId = _context.Categories.Where(o => o.Name == "Зимняя Спецодежда").FirstOrDefault().Id;
            var winterShoesId = _context.Categories.Where(o => o.Name == "Зимняя Обувь").FirstOrDefault().Id;
            var protectiveId = _context.Categories.Where(o => o.Name == "Защитная").FirstOrDefault().Id;
            var PVCandEVAId = _context.Categories.Where(o => o.Name == "ПВХ ЭВА").FirstOrDefault().Id;
            var PPEId = _context.Categories.Where(o => o.Name == "Средства Индивидуальной Защиты").FirstOrDefault().Id;
            var HPEId = _context.Categories.Where(o => o.Name == "Средства Защиты Рук").FirstOrDefault().Id;

            AllType typeList = new AllType();

            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.leathers, CategoryId = HPEId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.mittens, CategoryId = HPEId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.gloves, CategoryId = HPEId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.breath, CategoryId = PPEId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.eye, CategoryId = PPEId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.head, CategoryId = PPEId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.galoshes, CategoryId = PVCandEVAId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.stockingInsulated, CategoryId = winterShoesId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.bootsInsulated, CategoryId = winterShoesId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.shoesInsulated, CategoryId = winterShoesId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.feltShoes, CategoryId = winterShoesId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.slippers, CategoryId = summerShoesId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.semiBoots, CategoryId = summerShoesId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.boots, CategoryId = PVCandEVAId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.boots, CategoryId = summerShoesId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.shoes, CategoryId = summerShoesId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.signalVest, CategoryId = protectiveId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.apron, CategoryId = protectiveId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.suitFromHighTemperature, CategoryId = protectiveId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.suitFromAggressiveEnviroment, CategoryId = protectiveId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.waterProof, CategoryId = protectiveId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.weldingSuitInsulated, CategoryId = winterSuitId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.semiOveralls, CategoryId = winterSuitId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.pants, CategoryId = winterSuitId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.vest, CategoryId = winterSuitId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.jacket, CategoryId = winterSuitId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.knit, CategoryId = summerSuitId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.robe, CategoryId = summerSuitId });
            _context.TypeCategories.AddRange(new TypeCategory() { TypeId = typeList.suit, CategoryId = summerSuitId });

            _context.SaveChanges();
        }

        private void FillType()
        {
            _context.Types.AddRange(new Models.Items.Type() { Name = "Костюм" },
                                    new Models.Items.Type() { Name = "Халат" },
                                    new Models.Items.Type() { Name = "Трикотаж" },
                                    new Models.Items.Type() { Name = "Куртка" },
                                    new Models.Items.Type() { Name = "Жилет" },
                                    new Models.Items.Type() { Name = "Брюки" },
                                    new Models.Items.Type() { Name = "Полукомбинезоны" },
                                    new Models.Items.Type() { Name = "Костюмы сварщика утепленные" },
                                    new Models.Items.Type() { Name = "Влагозащитная" },
                                    new Models.Items.Type() { Name = "Спецодежда от кислот и агрессивной среды" },
                                    new Models.Items.Type() { Name = "Спецодежда от высоких температур" },
                                    new Models.Items.Type() { Name = "Фартуки" },
                                    new Models.Items.Type() { Name = "Сигнальные жилеты" },
                                    new Models.Items.Type() { Name = "Ботинки" },
                                    new Models.Items.Type() { Name = "Сапоги" },
                                    new Models.Items.Type() { Name = "Полуботинки" },
                                    new Models.Items.Type() { Name = "Тапочки" },
                                    new Models.Items.Type() { Name = "Ботинки утепленные" },
                                    new Models.Items.Type() { Name = "Сапоги утепленные" },
                                    new Models.Items.Type() { Name = "Обувь войлочная" },
                                    new Models.Items.Type() { Name = "Чулки утепляющие" },
                                    new Models.Items.Type() { Name = "Галоши" },
                                    new Models.Items.Type() { Name = "Головы" },
                                    new Models.Items.Type() { Name = "Зрения" },
                                    new Models.Items.Type() { Name = "Дыхания" },
                                    new Models.Items.Type() { Name = "Перчатки" },
                                    new Models.Items.Type() { Name = "Рукавицы" },
                                    new Models.Items.Type() { Name = "Краги" }
                                    );
            _context.SaveChanges();
        }

        private void FillItem
        (
            string _itemType = "Ботинки",
            string imgUrl = "None",
            string name = "Сапоги универсал",
            decimal price = (decimal)15.2,
            string lowDescription = "Короткие приветственные тексты",
            string HighDescription = "Для удобства мы разнесли «главные тексты» по 4 условным категориям. Сразу хотим предупредить, что категории это не обособленные: частенько можно встретить работы, которые вбирают в себя признаки сразу нескольких типов. Также здесь не упоминаются тексты для лендингов, где главная страница – основа основ. Речь пойдет об обычных, «классических» сайтах. Довольно демагогии с нашей стороны, приступаем.",
            string imgListUrl = "None"
        )
        {
            int itemType = _context.Types.Where(o => o.Name == _itemType).FirstOrDefault().Id;
            var item = new Item()
            {
                ImgUrl = imgUrl,
                ImgListUrl = imgListUrl,
                Name = name,
                Price = price,
                LowDescription = lowDescription,
                HighDescription = HighDescription,
            };
            var findItem = _context.Items.Where(o => o.Name == _itemType && o.Price == item.Price && o.HighDescription == item.HighDescription).FirstOrDefault();
            if (findItem == null)
            {
                _context.Items.Add(item);
                _context.SaveChanges();
                var itemId = _context.Items.Where(o => (o.Name == item.Name && o.Price == item.Price && o.HighDescription == item.HighDescription)).FirstOrDefault().Id;
                _context.ItemTypes.Add(new ItemType() { ItemId = itemId, TypeId = itemType });
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Не удалось добавить предмет, так как он уже существует");
            }
        }

        public void CreateChainOfItemAndSubType()
        {
            var items = from Items in _context.Items
                        join ItemType in _context.ItemTypes on Items.Id equals ItemType.ItemId
                        join Type in _context.Types on ItemType.TypeId equals Type.Id
                        orderby Items.Id
                        select new
                        {
                            ItemId = Items.Id,
                            TypeName = Type.Name
                        };
            foreach (var item in items)
            {
                switch (item.TypeName)
                {
                    case ("Ботинки утепленные"):
                    case ("Обувь войлочная"):
                    case ("Галоши"):
                    case ("Полуботинки"):
                    case ("Тапочки"):
                    case ("Ботинки"):
                        _context.ItemsSpecification.Add(new ItemsSpecification()
                        {
                            Name = "Ботинки",
                            ItemId = item.ItemId,
                            Color = "Черный",
                            Material = "Кожа",
                            MaterialInside = "Мех",
                            Model = "TX214",
                            Size = "39-46",
                            Weight = (decimal)0.215,
                            ProtectiveClass = "Отсутствует",
                            Character = "Отсутсвуют"
                        });
                        break;
                    case ("Сапоги"):
                    case ("Сапоги утепленные"):
                    case ("Чулки утепляющие"):
                        _context.ItemsSpecification.Add(new ItemsSpecification()
                        {
                            Name = "Сапоги",
                            ItemId = item.ItemId,
                            Color = "Черный",
                            Material = "Кожа",
                            MaterialInside = "Мех",
                            Model = "TX214",
                            Size = "39-46",
                            Height = "240",
                            Weight = (decimal)0.395,
                            ProtectiveClass = "Отсутствует",
                            Character = "Отсутсвуют"
                        });
                        break;
                    case ("Перчатки"):
                    case ("Рукавицы"):
                    case ("Краги"):
                        _context.ItemsSpecification.Add(new ItemsSpecification()
                        {
                            Name = "Перчатки",
                            ItemId = item.ItemId,
                            Color = "Черный",
                            Material = "Ткань",
                            Size = "20-25",
                            Weight = (decimal)0.025,
                            ProtectiveClass = "Отсутствует",
                            Character = "Отсутсвуют",
                            MatingClass = "15",
                            Thread = "10",
                            Model = "HSE214"
                        });
                        break;
                    case ("Дыхания"):
                    case ("Зрения"):
                    case ("Головы"):
                        _context.ItemsSpecification.Add(new ItemsSpecification()
                        {
                            Name = "СИЗ",
                            ItemId = item.ItemId,
                            Color = "Прозрачный",
                            Material = "Поликарбонат",
                            Size = "20-25",
                            Weight = (decimal)0.125,
                            ProtectiveClass = "Отсутствует",
                            Character = "Отсутсвуют",
                            MaterialInside = "Платик",
                            Model = "S18704"
                        });
                        break;
                    case ("Костюм"):
                    case ("Халат"):
                    case ("Трикотаж"):
                    case ("Куртка"):
                    case ("Жилет"):
                    case ("Брюки"):
                    case ("Полукомбинезоны"):
                    case ("Костюмы сварщика утепленные"):
                    case ("Влагозащитная"):
                    case ("Спецодежда от кислот и агрессивной среды"):
                    case ("Спецодежда от высоких температур"):
                    case ("Фартуки"):
                    case ("Сигнальные жилеты"):
                        _context.ItemsSpecification.Add(new ItemsSpecification()
                        {
                            Name = "СпецОдежда",
                            ItemId = item.ItemId,
                            Color = "Синий",
                            Material = "Поликарбонат",
                            Size = "20-25",
                            Weight = (decimal)0.125,
                            ProtectiveClass = "Отсутствует",
                            Character = "Отсутсвуют",
                            Growth = "160-190",
                            Model = "R36345F"
                        });
                        break;

                }
            }
            _context.SaveChanges();
        }

        class AllType
        {
            public int suit = _context.Types.Where(o => o.Name == "Костюм").FirstOrDefault().Id;
            public int robe = _context.Types.Where(o => o.Name == "Халат").FirstOrDefault().Id;
            public int knit = _context.Types.Where(o => o.Name == "Трикотаж").FirstOrDefault().Id;
            public int jacket = _context.Types.Where(o => o.Name == "Куртка").FirstOrDefault().Id;
            public int vest = _context.Types.Where(o => o.Name == "Жилет").FirstOrDefault().Id;
            public int pants = _context.Types.Where(o => o.Name == "Брюки").FirstOrDefault().Id;
            public int semiOveralls = _context.Types.Where(o => o.Name == "Полукомбинезоны").FirstOrDefault().Id;
            public int weldingSuitInsulated = _context.Types.Where(o => o.Name == "Костюмы сварщика утепленные").FirstOrDefault().Id;
            public int waterProof = _context.Types.Where(o => o.Name == "Влагозащитная").FirstOrDefault().Id;
            public int suitFromAggressiveEnviroment = _context.Types.Where(o => o.Name == "Спецодежда от кислот и агрессивной среды").FirstOrDefault().Id;
            public int suitFromHighTemperature = _context.Types.Where(o => o.Name == "Спецодежда от высоких температур").FirstOrDefault().Id;
            public int apron = _context.Types.Where(o => o.Name == "Фартуки").FirstOrDefault().Id;
            public int signalVest = _context.Types.Where(o => o.Name == "Сигнальные жилеты").FirstOrDefault().Id;
            public int shoes = _context.Types.Where(o => o.Name == "Ботинки").FirstOrDefault().Id;
            public int boots = _context.Types.Where(o => o.Name == "Сапоги").FirstOrDefault().Id;
            public int semiBoots = _context.Types.Where(o => o.Name == "Полуботинки").FirstOrDefault().Id;
            public int slippers = _context.Types.Where(o => o.Name == "Тапочки").FirstOrDefault().Id;
            public int shoesInsulated = _context.Types.Where(o => o.Name == "Ботинки утепленные").FirstOrDefault().Id;
            public int bootsInsulated = _context.Types.Where(o => o.Name == "Сапоги утепленные").FirstOrDefault().Id;
            public int feltShoes = _context.Types.Where(o => o.Name == "Обувь войлочная").FirstOrDefault().Id;
            public int stockingInsulated = _context.Types.Where(o => o.Name == "Чулки утепляющие").FirstOrDefault().Id;
            public int galoshes = _context.Types.Where(o => o.Name == "Галоши").FirstOrDefault().Id;
            public int head = _context.Types.Where(o => o.Name == "Головы").FirstOrDefault().Id;
            public int eye = _context.Types.Where(o => o.Name == "Зрения").FirstOrDefault().Id;
            public int breath = _context.Types.Where(o => o.Name == "Дыхания").FirstOrDefault().Id;
            public int gloves = _context.Types.Where(o => o.Name == "Перчатки").FirstOrDefault().Id;
            public int mittens = _context.Types.Where(o => o.Name == "Рукавицы").FirstOrDefault().Id;
            public int leathers = _context.Types.Where(o => o.Name == "Краги").FirstOrDefault().Id;
        }
    }
}
