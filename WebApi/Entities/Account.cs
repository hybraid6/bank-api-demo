namespace WebApi.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public decimal Balance { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
