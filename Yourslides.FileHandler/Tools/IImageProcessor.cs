namespace Yourslides.FileHandler.Tools {
    public interface IImageProcessor {
        void Process(string filepath);
        void CreateSubdirectories(string outputdir);
    }
}