public static class GameState
{
	public static int CURRENT_LEVEL = 0;
	public static int CURRENT_HP = 100;
	public static readonly int MAX_HP = 100;
	public static int CURRENT_DIFFICULTY = 1;
	public static int CURRENT_ACTIVE_ENEMIES_COUNT = 0;

	public static int CURRENT_EXP = 0;

	public static float HP_DROP_RATE = 10f;

	public static bool IS_SWORD_ACTIVE = false;
	public static float LAST_SWORD_USE_TIME = 0;

	public static float MAGNET_DROP_RATE = 5f;

	public static void reset_game_state(){
		CURRENT_HP = MAX_HP;
		CURRENT_LEVEL = 0;
		CURRENT_DIFFICULTY = 1;
		CURRENT_EXP = 0;
 CURRENT_ACTIVE_ENEMIES_COUNT = 0;
    }
}
