
namespace Enum {
    namespace Sound
    {
        public enum BGM_Enum
        {
            TITLE = 0,          //タイトル
            PLAY,             //ゲームプレイ中１
            ENDING,             //エンディング
            None,               //何も再生していない
        }

        public enum SE_Enum
        {
            SHOT_1 = 0,         //弾発射音(ビーム系)
            SHOT_2,             //弾発射音(弾系)
            NEEDLE,
            WIND,
            ROCK,
            FALL,
            MOVE,
            KABEUP,
            MISS,
            SHOT_CHANGE,
            GOAL,
        }
    }
}
