using Horus.Domain.SeedWork;

namespace Horus.Domain.Findings.Notes
{
	public interface IAnnotable
	{
		public IReadOnlyCollection<Note> Notes { get; }

		public void AddNote(Note note);

		public void RemoveNote(Note note);
	}
}