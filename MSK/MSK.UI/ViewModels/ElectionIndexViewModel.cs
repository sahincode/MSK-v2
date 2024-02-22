using MSK.Business.DTOs.ElectionModelDTOs;
using MSK.Business.DTOs.HomeSlideDTOs;

namespace MSK.UI.ViewModels
{
    public class ElectionIndexViewModel
    {
        public List<  HomeSlideLayoutDto> HomeSlideLayoutDtos { get; set; }
        public ElectionLayoutDto ElectionLayoutDto { get; set; }
    }
}
