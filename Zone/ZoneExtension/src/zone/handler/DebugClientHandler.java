package zone.handler;
import java.util.Random;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class DebugClientHandler extends BaseClientRequestHandler{
	public static long gold = 100;
	private static int timegame = 15;
	private boolean reset;
	@Override
	public void handleClientRequest (User u, ISFSObject dt) {		
		//trace("Start");
		ISFSObject data = new SFSObject();	
		String log = dt.getUtfString("logDebug"); 		
		Boolean demnguoc = dt.getBool("demNguoc");	
		reset = dt.getBool("reset");
		//trace(demnguoc);
		if(timegame != 0)
		{if(demnguoc)
			{
				timegame --;				
				data.putInt("timegame", timegame);
				demnguoc = false;
				this.send("ThongBaoTime", data, u);
			}
		}
		else if(timegame == 0)
		{			
			this.gold -= 10;
			//trace("Gold - 10 : " + this.gold );
			timegame = 15;
			demnguoc = false;		
			data.putLong("gold", this.gold);
			data.putInt("timegame", timegame);
			this.send("Thongbaoserver", data, u);			
		}	
		if(reset)
		{
			trace(reset);
			timegame = 15;
			data.putInt("timegame", timegame);
			demnguoc = false;
			reset = false;
			this.send("ThongBaoTime", data, u);
			trace("gui du lieu reset time");
		}
	}

	public long getGold() {
		return  gold;
	}

	public void setGold(long gold) {
		this.gold = gold;
	}
	
	public int gettimegame() {
		return  timegame;
	}

	public void settimegame(int timegame) {
		this.timegame =  timegame;
	}
	
}
