using Horus.Domain.SeedWork;

namespace Horus.Domain.Findings.Notes
{
	public class AnnotableEntity : Entity
	{
		// Backing Field
		private readonly List<Note> _notes = [];

		// Relation
		public IReadOnlyCollection<Note> Notes => _notes.AsReadOnly();

		public void AddNote(Note note)
		{
			if (note is not null)
				_notes.Add(note);
		}

		public void RemoveNote(Note note)
		{
			_notes.Remove(note);
		}
	}
}