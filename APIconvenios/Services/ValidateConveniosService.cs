
using APIconvenios.Common;
using APIconvenios.DTOs.ConvenioEspecifico;
using APIconvenios.DTOs.ConvenioMarco;
using APIconvenios.DTOs.Convenios;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.UnitOfWork;

namespace APIconvenios.Services
{
    public class ValidateConveniosService : IValidateConveniosService
    {
        private readonly _UnitOfWork _UnitOfWork;

        public ValidateConveniosService(_UnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<Result<object?>> ValidateCargaConvenioEspecifico(CargarConvenioEspecificoRequestDto _Dto)
        {
            var task1 = _UnitOfWork._ConvEspReadRepository.TitleConvenioExist(_Dto.InsertConvenioDto.Titulo);

            Task<Result<object?>>? task2 = null;
            Task<Result<object?>>? task3 = null;
            

            if (_Dto.InsertConvenioDto.numeroconvenio != null)
            {
                task2 = _UnitOfWork._ConvEspReadRepository.NumeroConvenioExist(_Dto.InsertConvenioDto.numeroconvenio);
            }

            if (_Dto.InsertEmpresaDto != null && _Dto.InsertEmpresaDto.Id == null)
            {
                task3 = _UnitOfWork._EmpresaRepository.NameEmpresaExist(_Dto.InsertEmpresaDto.Nombre);
            }

            var tasks = new List<Task<Result<object?>>>();
            if (task1 != null) tasks.Add(task1);
            if (task2 != null) tasks.Add(task2);
            if (task3 != null) tasks.Add(task3);

            await Task.WhenAll(tasks);

            var result1 = await task1;

            if (!result1.Exit) return Result<object?>.Error(result1.Errormessage, result1.Errorcode);

            if (task2 != null)
            {
                var result2 = await task2;
                if(!result2.Exit) return Result<object?>.Error(result2.Errormessage, result2.Errorcode);
            }

            if (task3 != null)
            {
                var result3 = await task3;
                if (!result3.Exit) return Result<object?>.Error(result3.Errormessage, result3.Errorcode);
            }
            

            return Result<object?>.Exito(null);  
        }

        public async Task<Result<object?>> ValidateCargaConvenioMarco(CargarConvenioMarcoRequestDto _Dto)
        {
            var task1 = _UnitOfWork._ConvenioMarcoReadRepository.TitleConvenioExist(_Dto.InsertConvenioDto.Titulo);

            Task<Result<object?>>? task2 = null;
            Task<Result<object?>>? task3 = null;


            if (_Dto.InsertConvenioDto.numeroconvenio != null)
            {
                task2 = _UnitOfWork._ConvenioMarcoReadRepository.NumeroConvenioExist(_Dto.InsertConvenioDto.numeroconvenio);
            }

            if (_Dto.InsertEmpresaDto != null && _Dto.InsertEmpresaDto.Id == null)
            {
                task3 = _UnitOfWork._EmpresaRepository.NameEmpresaExist(_Dto.InsertEmpresaDto.Nombre);
            }

            var tasks = new List<Task<Result<object?>>>();
            if (task1 != null) tasks.Add(task1);
            if (task2 != null) tasks.Add(task2);
            if (task3 != null) tasks.Add(task3);

            await Task.WhenAll(tasks);

            var result1 = await task1;

            if (!result1.Exit) return Result<object?>.Error(result1.Errormessage, result1.Errorcode);

            if (task2 != null)
            {
                var result2 = await task2;
                if (!result2.Exit) return Result<object?>.Error(result2.Errormessage, result2.Errorcode);
            }

            if (task3 != null)
            {
                var result3 = await task3;
                if (!result3.Exit) return Result<object?>.Error(result3.Errormessage, result3.Errorcode);
            }


            return Result<object?>.Exito(null);
        }

        public async Task<Result<object?>> ValidateUpdateConvenioEspecifico(UpdateConvenioEspecificoRequestDto _Dto)
        {
            var task1 = _UnitOfWork._ConvEspReadRepository.
                TitleConvenioExistForUpdate(_Dto.UpdateConvenioDto.Titulo, _Dto.UpdateConvenioDto.Id);

            Task<Result<object?>>? task2 = null;
            Task<Result<object?>>? task3 = null;


            if (_Dto.UpdateConvenioDto.numeroconvenio != null)
            {
                task2 = _UnitOfWork._ConvEspReadRepository
                    .NumeroConvenioExistForUpdate(_Dto.UpdateConvenioDto.numeroconvenio, _Dto.UpdateConvenioDto.Id);
            }

            if (_Dto.InsertEmpresaDto != null && _Dto.InsertEmpresaDto.Id == null)
            {
                task3 = _UnitOfWork._EmpresaRepository.NameEmpresaExist(_Dto.InsertEmpresaDto.Nombre);
            }

            var tasks = new List<Task<Result<object?>>>();
            if (task1 != null) tasks.Add(task1);
            if (task2 != null) tasks.Add(task2);
            if (task3 != null) tasks.Add(task3);

            await Task.WhenAll(tasks);

            var result1 = await task1;

            if (!result1.Exit) return Result<object?>.Error(result1.Errormessage, result1.Errorcode);

            if (task2 != null)
            {
                var result2 = await task2;
                if (!result2.Exit) return Result<object?>.Error(result2.Errormessage, result2.Errorcode);
            }

            if (task3 != null)
            {
                var result3 = await task3;
                if (!result3.Exit) return Result<object?>.Error(result3.Errormessage, result3.Errorcode);
            }


            return Result<object?>.Exito(null);
        }

        public async Task<Result<object?>> ValidateUpdateConvenioMarco(UpdateConvenioMarcoRequetsDto _Dto)
        {
            var task1 = _UnitOfWork._ConvenioMarcoReadRepository.
                TitleConvenioExistForUpdate(_Dto.UpdateConvenioMarcoDto.Titulo, _Dto.UpdateConvenioMarcoDto.Id);

            Task<Result<object?>>? task2 = null;
            Task<Result<object?>>? task3 = null;


            if (_Dto.UpdateConvenioMarcoDto.numeroconvenio != null)
            {
                task2 = _UnitOfWork._ConvenioMarcoReadRepository
                    .NumeroConvenioExistForUpdate(_Dto.UpdateConvenioMarcoDto.numeroconvenio, _Dto.UpdateConvenioMarcoDto.Id);
            }

            if (_Dto.InsertEmpresaDto != null && _Dto.InsertEmpresaDto.Id == null)
            {
                task3 = _UnitOfWork._EmpresaRepository.NameEmpresaExist(_Dto.InsertEmpresaDto.Nombre);
            }

            var tasks = new List<Task<Result<object?>>>();
            if (task1 != null) tasks.Add(task1);
            if (task2 != null) tasks.Add(task2);
            if (task3 != null) tasks.Add(task3);

            await Task.WhenAll(tasks);

            var result1 = await task1;

            if (!result1.Exit) return Result<object?>.Error(result1.Errormessage, result1.Errorcode);

            if (task2 != null)
            {
                var result2 = await task2;
                if (!result2.Exit) return Result<object?>.Error(result2.Errormessage, result2.Errorcode);
            }

            if (task3 != null)
            {
                var result3 = await task3;
                if (!result3.Exit) return Result<object?>.Error(result3.Errormessage, result3.Errorcode);
            }


            return Result<object?>.Exito(null);
        }
    }
}
