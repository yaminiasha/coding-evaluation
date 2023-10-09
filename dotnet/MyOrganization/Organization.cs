using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            Position? position = FindPositionByTitle(root, title);

            if (position != null)
            {
                Employee employee = new Employee(position.GetDirectReports().Count , person);
                position.SetEmployee(employee);
                return position;
            }
            //your code here
            return null;
        }

        /// <summary>
        /// Find Position of the by Title from the Root of the Organization
        /// </summary>
        /// <param name="root"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public Position? FindPositionByTitle(Position root, string title)
        {
            if (root == null)
            {
                return null;
            }

            if (root.GetTitle() == title)
            {
                return root;
            }

            foreach (Position sub in root.GetDirectReports())
            {
                Position? foundPosition = FindPositionByTitle(sub, title);
                if (foundPosition != null)
                {
                    return foundPosition;
                }
            }

            return null;
        }


        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "  "));
            }
            return sb.ToString();
        }
    }
}
