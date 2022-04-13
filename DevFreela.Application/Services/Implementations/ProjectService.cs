using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.DTO;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Services;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IPaymentService _paymentService;
        public ProjectService(DevFreelaDbContext dbContext, IPaymentService paymentService)
        {
            _dbContext = dbContext;
            _paymentService = paymentService;
        }

        public async Task<bool> Finish(PaymentInfoDTO paymentInfoDTO)
        {
            var project = _dbContext.Projects.SingleOrDefault(projectDb => projectDb.Id == paymentInfoDTO.IdProejct);

            project.Finish();

            var result = await _paymentService.ProcessPayment(paymentInfoDTO);

            if (!result)
            {
                project.SetPaymentPending();
            }

            await _dbContext.SaveChangesAsync();

            return result;
        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(projectDb => projectDb.Id == id);

            project.Start();

            _dbContext.SaveChanges();
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(projectDb => projectDb.Id == inputModel.Id);

            project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);

            _dbContext.SaveChanges();
        }
    }
}
