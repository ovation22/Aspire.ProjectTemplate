using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Aspire.ProjectTemplate.Infrastructure.Persistence;

/// <summary>
///     Abstract repository class providing basic CRUD operations.
/// </summary>
public class ProjectTemplateRepository : EFRepository
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ProjectTemplateRepository" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    public ProjectTemplateRepository(ProjectTemplateContext dbContext, ILogger<ProjectTemplateRepository> logger, IMapper? mapper)
        : base(dbContext, logger, mapper)
    {
    }
}
