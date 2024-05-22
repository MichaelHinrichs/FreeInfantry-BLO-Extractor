namespace FreeeInfantry_BLO_Extractor
{
    internal class Program
    {
        public static BinaryReader br;
        static void Main(string[] args)
        {
            br = new(File.OpenRead(args[0]));
            br.ReadInt32();
            int fileCount = br.ReadInt32();

            List<Subfile> subfiles = new();
            for (int i = 0; i < fileCount; i++)
            {
                subfiles.Add(new());
            }

            string path = Path.GetDirectoryName(args[0]) + "\\" + Path.GetFileNameWithoutExtension(args[0]);
            Directory.CreateDirectory(path);
            foreach (Subfile file in subfiles)
            {
                br.BaseStream.Position = file.start;
                BinaryWriter bw = new(File.Create(path + "\\" + file.name[0]));
                bw.Write(br.ReadBytes(file.size));
                bw.Close();
            }
        }

        class Subfile
        {
            public string[] name = new string(br.ReadChars(32)).TrimEnd('\0').Split('\0');
            public int start = br.ReadInt32();
            public int size = br.ReadInt32();
        }
    }
}
