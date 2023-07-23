using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.TransactionRepo;
using WMSAPI.DTO;

namespace WMSAPI.Controllers
{
    [Authorize]
    //[Route("api/[controller]")]
    //[ApiController]
    public class TransactionsController : ODataController
    {
        private readonly ITransactionRepository repository;
        private IMapper Mapper { get; }

        public TransactionsController(IMapper mapper)
        {
            Mapper = mapper;
            repository = new TransactionRepository();
        }

        [Authorize(Roles = "ADMIN")]
        [EnableQuery]
        public ActionResult<List<TransactionDTO>> Get()
        {
            return Ok(Mapper.Map<List<TransactionDTO>>(repository.GetTransactions()));
        }
        [Authorize(Roles = "EMPLOYEE")]
        [HttpGet]
        [Route("api/[controller]/GetAllTransactionsByUser")]
        public ActionResult<List<TransactionDTO>> GetAllTransactionsByUser(int? id = 2)
        {
            return Ok(Mapper.Map<List<TransactionDTO>>(repository.GetTransactionsByUserId(id)));
        }
        [Authorize(Roles = "ADMIN, EMPLOYEE")]
        [EnableQuery]
        public ActionResult<TransactionDTO> Get([FromODataUri] int key)
        {
            var transaction = Mapper.Map<TransactionDTO>(repository.GetTransactionById(key));

            if (transaction == null)
            {
                return new NotFoundResult();
            }

            return Ok(transaction);
        }
        [Authorize(Roles = "ADMIN, EMPLOYEE")]
        [EnableQuery]
        public IActionResult Post([FromBody] TransactionRequestDTO transactionRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = Mapper.Map<Transaction>(transactionRequest);
            repository.SaveTransaction(transaction);

            return Ok(Mapper.Map<TransactionRequestDTO>(repository.GetTransactionByLastId()));
        }
        [Authorize(Roles = "ADMIN, EMPLOYEE")]
        [EnableQuery]
        public IActionResult Put([FromODataUri] int key, [FromBody] TransactionRequestDTO transactionRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _transaction = repository.GetTransactionById(key);
            var transaction = Mapper.Map<Transaction>(transactionRequest);

            if (_transaction == null || transaction.Id != key)
            {
                return new NotFoundResult();
            }

            repository.UpdateTransaction(transaction);

            return Ok(Mapper.Map<TransactionRequestDTO>(repository.GetTransactionById(key)));
        }
        [Authorize(Roles = "ADMIN")]
        [EnableQuery]
        public IActionResult Delete([FromODataUri] int key)
        {
            var transaction = repository.GetTransactionById(key);
            var transactionResponse = Mapper.Map<TransactionDTO>(transaction);

            if (transaction == null)
            {
                return new NotFoundResult();
            }

            repository.DeleteTransaction(transaction);

            return Ok(transactionResponse);
        }
    }
}
