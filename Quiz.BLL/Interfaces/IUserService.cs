using Quiz.BLL.DTO;
using Quiz.BLL.DTO.User;
using Quiz.BLL.DTO.UserResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quiz.BLL.Interfaces
{
    public interface IUserService
    {
		Task<UserDTO> GetById(string id);

		Task<IEnumerable<UserDTO>> GetAll();

		Task<UserDTO> Delete(string id);

		Task<UserDTO> Update(UserDTO userDTO);

		Task<PagedResultDTO<UserDTO>> GetPaged(string query,
												int page = 1,
												int pageSize = 10);

		IEnumerable<TestResultDTO> GetAllTests(string id);

		void Dispose();
    }
}
