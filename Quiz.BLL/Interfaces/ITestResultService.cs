using Quiz.BLL.DTO;
using Quiz.BLL.DTO.UserResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quiz.BLL.Interfaces
{
	public interface ITestResultService
	{
		Task<TestResultDTO> Create(TestResultDTO testResultDTO);

		Task<TestResultDTO> Delete(int id);

		TestResultDTO Get(int id);

		IEnumerable<TestResultDTO> GetAll();

		PagedResultDTO<TestResultDTO> GetPaged(
			int page = 1,
			int pageSize = 10);

		void Dispose();
	}
}
