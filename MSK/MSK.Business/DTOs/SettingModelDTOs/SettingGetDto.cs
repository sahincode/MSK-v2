namespace MSK.Business.DTOs.SettingModelDTOs
{
    public class SettingGetDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
