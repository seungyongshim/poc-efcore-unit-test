using System.ComponentModel.DataAnnotations.Schema;

namespace SendMail.EfCore;

public record ServicePermission(ApiKey Id,
                                ServicePermissionData Data);
