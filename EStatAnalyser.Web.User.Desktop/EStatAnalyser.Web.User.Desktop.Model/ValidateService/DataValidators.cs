namespace EStatAnalyser.Web.User.Desktop.Model.ValidateService
{
    public class DataValidators
    {

        public static bool CheckFileContent(string content, bool IsUploadFile)
        {
            if (IsUploadFile)
            {
                return CheckUploadFile(content);
            }
            else
            {
                return CheckSimpleFile(content);
            }
        } 

        private static bool CheckUploadFile(string content)
        {
            try
            {
                var Result = true;

                var strings = content.Split('\n');

                var counter = 0; // нулевая строка - заголовок данных

                foreach (var item in strings)
                {
                    if (counter != 0)
                    {
                        var splits = item
                            .Replace('.',',')
                            .Split('\t');
                        var i_num = 0;
                        var d_num = 0.0;
                        if (!(splits.Length == 2 
                                && (Int32.TryParse(splits[0], out i_num) || Double.TryParse(splits[0], out d_num))
                                && (Int32.TryParse(splits[1], out i_num) || Double.TryParse(splits[1], out d_num))
                             )
                           )
                        {
                            if (counter != strings.Length - 1)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        var splits = item.Split('\t');
                        bool var_bool;
                        if (splits.Length != 5 && !Boolean.TryParse(splits[4], out var_bool))
                        {
                            return false;
                        }
                    }
                    counter++;
                }

                return Result;
            }
            catch { return false; }
        }

        private static bool CheckSimpleFile(string content)
        {
            try
            {
                var Result = true;

                var strings = content.Split('\n');

                var counter = 0; // нулевая строка - заголовок данных

                foreach (var item in strings)
                {
                    if (counter != 0)
                    {
                        var splits = item
                            .Replace('.', ',')
                            .Split('\t');
                        var i_num = 0;
                        var d_num = 0.0;
                        if (!(splits.Length == 2
                                && (Int32.TryParse(splits[0], out i_num) || Double.TryParse(splits[0], out d_num))
                                && (Int32.TryParse(splits[1], out i_num) || Double.TryParse(splits[1], out d_num))
                             )
                           )
                        {
                            if (counter != strings.Length - 1)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        var splits = item.Split('\t');
                        if (splits.Length != 2)
                        {
                            return false;
                        }
                    }
                    counter++;
                }

                return Result;
            }
            catch { return false; }
        }
    }
}