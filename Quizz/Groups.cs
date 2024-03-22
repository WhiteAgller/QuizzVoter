using System.Collections.Generic;

namespace Quizz;

public class Group
{
    public char Key { get; set; }
    public string[] Participants { get; set; }
}

public class GroupList
{
    public List<Group> Groups { get; set; }
}