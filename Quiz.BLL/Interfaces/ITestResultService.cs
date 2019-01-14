using Quiz.BLL.DTO.UserResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quiz.BLL.Interfaces
{
	public interface ITestResultService
	{
		Task<TestResultDTO> Create(TestResultDTO testResultDTO);

		Task<TestResultDTO> Delete(int id);

		Task<TestResultDTO> Get(int id);

		Task<IEnumerable<TestResultDTO>> GetAll();

		void Dispose();
	}
}
