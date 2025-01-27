namespace BlogManagementDomain.Base
{
    public abstract class BaseDomain
    {
        #region Properties
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        #endregion
    }
}
