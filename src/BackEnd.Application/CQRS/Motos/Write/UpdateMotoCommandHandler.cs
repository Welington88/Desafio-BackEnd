﻿using BackEnd.Application.Dtos.Motos;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.Domain.SeedWork;
using HunterDomain.Extensions;
using MediatR;

namespace BackEnd.Application.CQRS.Motos.Write;

public class UpdateMotoCommandHandler : IRequestHandler<UpdateMotoCommand, Moto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryDapper _repositoryDapper;
    private readonly Queries _query = new();

    public UpdateMotoCommandHandler(IUnitOfWork unitOfWork, IRepositoryDapper repositoryDapper)
    {
        _unitOfWork = unitOfWork;
        _repositoryDapper = repositoryDapper;
    }

    public async Task<Moto> Handle(UpdateMotoCommand request, CancellationToken cancellationToken)
    {
        var query = _query.GetQuery((int)QueryCQRS.QueryMotosById);
        var parameters = new
        {
            Id = request.Id
        };
        var motoUpdate = await _repositoryDapper.GetById<Moto>(request.Id, query, parameters);

        if (motoUpdate is null)
            throw new Exception("Moto is Null");

        motoUpdate.Update(request.Ano, request.Modelo, request.Placa, true);

        await _unitOfWork.Repository.UpdateObject<Moto>(motoUpdate);

        await _unitOfWork.CommitAsync();

        return motoUpdate;
    }
}