
namespace Kameffee.AudioPlayer
{
    public interface IBgmBundle
    {
        BgmData GetData(int index);

        BgmData GetData(string name);
    }
}
