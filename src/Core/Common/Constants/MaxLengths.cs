namespace Core.Common.Constants
{
    public static class MaxLengths
    {
        public static class User
        {
            public const int Name = 50;
            public const int Password = 50;
            public const int Email = 200;
            public const int PhoneNumber = 20;
        }

        public static class Shop
        {
            public const int Name = 50;
            public const int Description = 300;
        }

        public static class Product
        {
            public const int Name = 50;
            public const int Description = 500;
        }

        public static class Order
        {
            public const int Description = 250;
        }
        public class OrderProduct
        {
            public const int Quantity = 100;
        }

        public static class Category
        {
            public const int Name = 30;
        }

        public class Address
        {
            public const int AdressLine = 150;
            public const int City = 50;
            public const int Street = 100;
            public const int ZipCode = 5;
        }

    }
}
