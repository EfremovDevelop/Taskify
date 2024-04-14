using System;
namespace Taskify.Core.Models;

	public class VersionIssue
	{
		private VersionIssue(Guid id, string version)
		{
			Id = id;
			Version = version;
		}

    public Guid Id { get; }
    public string Version { get; }

		public static (VersionIssue, string) Create(Guid id, string version)
		{
			string err = string.Empty;

			if (string.IsNullOrEmpty(version))
				err = "Error. Version is empty";

			var versionIssue = new VersionIssue(id, version);

			return (versionIssue, err);
    }
}