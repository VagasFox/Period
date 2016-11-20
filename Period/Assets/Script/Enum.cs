
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
    namespace Gimmick{
        public enum GimmickType
        {
            GRAVITY,    //重力
            HARDNESS,   //硬さ
            SPEED,      //速さ
            ATTACK,     //攻撃力
            DOOR,       //ドア
            LIGHT,      //ライト
            ROTATE,     //回転
            SIZE,       //大きさ
            NEEDLE,     //とげ
            WIND,       //風
            NONE,       //何も無し
        }
    }
}
