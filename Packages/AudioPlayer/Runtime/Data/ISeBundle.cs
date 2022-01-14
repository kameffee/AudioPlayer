namespace Kameffee.AudioPlayer
{
    public interface ISeBundle
    {
        SeData GetData(int index);

        SeData GetData(string key);
    }
}