using System;
using System.Collections.Generic;

// Comment for review pull request

namespace CodingChallenge.FamilyTree
{
    public class Solution
    {
        public string TheRealBirthday { get; set; }

        public string GetBirthMonth(Person person, string descendantName)
        {
            // Added ToString() having issues with debugger evaluating "Name1" == "Name1" as false.....
            // If the initial Person's name is the descendant's name set TheRealBirthday to that individuals month
            if (person.Name.ToString() == descendantName.ToString())
            {
                return TheRealBirthday = person.Birthday.ToString("MMMM");
            }

            // This loop is going to loop through the initial descendant's of the person provided.
            // for each initial descendant I check to see if they are the one in question if not we check to
            // see if they have any descendants, if they do we call KeepLooking which is going to traverse
            // down the current branch of the tree.
            foreach (Person peep in person.Descendants)
            {
                if (peep.Name == descendantName)
                {
                    TheRealBirthday = peep.Birthday.Month.ToString();
                } else if (peep.Descendants.Count > 0)
                {
                    this.KeepLooking(peep, descendantName);
                }
            }

            // I Thought returning a null property of type string defaulted to "" but it was not.
            // So I added the following condition to return "" if TheRealBirthday property was null
            if (TheRealBirthday == null)
            {
                return "";
            }

            // Returns the value of TheRealBirthday if a match was found
            return TheRealBirthday;
        }

        public void KeepLooking (Person person, string descendantName)
        {
            // The foreach loop in this function takes care of the a person having a descendant 
            // with a sibling. Essentially for each level we go down the tree, this foreach will 
            // represent the number of people at the current level on the given branch.
            foreach (Person man in person.Descendants)
            {
                if (man.Name == descendantName)
                {
                    TheRealBirthday = man.Birthday.ToString("MMMM");
                } else if (man.Descendants.Count > 0)
                {
                    // By continuing to call itself (KeepLooking) we are able to drill all the way
                    // down a given branch while still capturing the number of people at each level
                    // within the consecutive foreach's
                    this.KeepLooking(man, descendantName);
                } else
                {
                    continue;
                }
            }

            // if a man.Name ever matches the descendantName, TheRealBirthday property on the Solution
            // class will be set to the month of their birthday.

            /* Something to note, because this method will run all the way through the tree even
             after finding the match. If there are multiple descendants with the same name TheRealBirthday
             will equal the month of the last match found.
             */

            return;            
        }
    }
}