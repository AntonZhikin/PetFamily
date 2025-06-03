using System.Linq.Expressions;
using FilesService.Communication;
using FilesService.Contract;
using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs;
using PetFamily.Core.DTOs.Pets;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;

namespace PetFamily.Pets.Application.PetManagement.Queries.GetPetsWithPagination;

public class GetPetsWithPaginationHandler : IQueryHandler<PagedList<PetDto>, GetPetsWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly FileHttpClient _httpClient;

    public GetPetsWithPaginationHandler(IReadDbContext readDbContext, FileHttpClient httpClient)
    {
        _readDbContext = readDbContext;
        _httpClient = httpClient;
    }
    
    public async Task<PagedList<PetDto>> Handle(GetPetsWithPaginationQuery query, 
        CancellationToken cancellationToken = default)
    {
        var petQuery = _readDbContext.Pets.AsQueryable();

        Expression<Func<PetDto, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "name" => (pet) => pet.Name,
            "color" => (pet) => pet.Color,
            "VolunteerId" => (pet) => pet.VolunteerId,
            "SpeciesId" => (pet) => pet.SpeciesBreedDto.SpeciesId,
            "BreedId" => (pet) => pet.SpeciesBreedDto.BreedId,
            _ => p => p.Id
        };
        
        petQuery = query.SortDirection?.ToLower() == "desc" 
            ? petQuery.OrderByDescending(keySelector) 
            : petQuery.OrderBy(keySelector);
        
        petQuery = petQuery.WhereIf(query.VolunteerId != null,
            p => p.VolunteerId == query.VolunteerId);
        
        petQuery = petQuery.WhereIf(
            !string.IsNullOrEmpty(query.Name),
            p => p.Name == query.Name);

        petQuery = petQuery.WhereIf(
            query.IsVaccine != null,
            p => p.IsVaccine == query.IsVaccine);
        
        petQuery = petQuery.WhereIf(
            query.IsNeutered != null,
            p => p.IsNeutered == query.IsNeutered);
        
        petQuery = petQuery.WhereIf(query.SpeciesId != null,
            p => p.SpeciesBreedDto.SpeciesId == query.SpeciesId);
        
        petQuery = petQuery.WhereIf(query.BreedId != null,
            p => p.SpeciesBreedDto.BreedId == query.BreedId);
        
        petQuery = petQuery.WhereIf(
            !string.IsNullOrEmpty(query.Color),
            p => p.Color == query.Color);
        
        var result =  await petQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
        
        /*foreach (var petDto in result.Items)
        {
            var getAvatarUrlResult = await _httpClient.GetFilesPresignedUrls(
                petDto.Avatar.FileName,
                new GetFilesPresignedUrlsRequest(petDto.Avatar.BucketName),
                cancellationToken);
            if (getAvatarUrlResult.IsSuccess)
                petDto.AvatarUrl = getAvatarUrlResult.Value.Url;
        
            List<string> photosUrls = [];
            foreach (var photo in petDto.Photos)
            {
                var getPresignedPhotoUrlRequest = new GetPresignedUrlRequest(Constants.BUCKET_NAME_PHOTOS);
                var getPhotoUrlResult = await _httpClient.GetPresignedUrl(
                    photo.FileName,
                    getPresignedPhotoUrlRequest,
                    cancellationToken);
                if(getPhotoUrlResult.IsSuccess)
                    photosUrls.Add(getPhotoUrlResult.Value.Url);
            }
            petDto.PhotosUrls = photosUrls;
        }*/
        return result;
    }
}