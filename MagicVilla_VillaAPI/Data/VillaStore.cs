using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto> {  new VillaDto {Id=1,Name="Pool View"},
                new VillaDto {Id=1,Name="Beach View",Occupancy=4,Sqft=500},
                new VillaDto {Id =1,Name="Purple House",Occupancy=5,Sqft=300}};
    }
}
