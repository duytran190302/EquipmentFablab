using Fablab.Models.Domain;
using Fablab.Models.DTO;
using AutoMapper;

namespace Fablab.Helpers.Mapper
{
	public class MappingConf
	{
		public MappingConf() 
		{
			CreateMap<Borrow,BorrowDTO>();

		}

	}
}
