namespace VKR2.Domain.DTOs
{
    public class UsersUpdateDTO
    {
        public string Email { get; set; }
        public string Passwordhash { get; set; } // Изменили на чистый пароль (не хеш)
    }

    public class UsersCreateDTO : UsersUpdateDTO
    {
        public string RoleName { get; set; } // Теперь передаем название роли вместо ID
        public int? Parentcd { get; set; }
        public int? Workercd { get; set; }
    }

    public class UsersDTO : UsersCreateDTO
    {
        public int Usercd { get; set; }
    }
}