using AutoMapper;
using System.Reflection;

namespace Game.Messaging.Server.Application.Common.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile(IEnumerable<Assembly> assemblies)
		{
			AllowNullCollections = true;
			foreach (var assembly in assemblies)
			{
				ApplyMappingsFromAssembly(assembly);
			}
		}

		public MappingProfile()
		{
			AllowNullCollections = true;
			ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
		}

		private void ApplyMappingsFromAssembly(Assembly assembly)
		{
			var typesWithMapFrom = assembly.GetExportedTypes()
				.Where(t => t.GetInterfaces().Any(i =>
					i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
				.ToList();

			foreach (var type in typesWithMapFrom)
			{
				var instance = Activator.CreateInstance(type);

				var methodInfo = type.GetMethod("Mapping")
					?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping");

				methodInfo?.Invoke(instance, new object[] { this });
			}
		}
	}
}
