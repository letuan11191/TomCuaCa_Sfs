package zone.handler;

import java.util.Random;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class ChooseDiceHandler extends BaseClientRequestHandler{
	

	@Override
	public void handleClientRequest (User u, ISFSObject dt) {
		//trace("ChooseDiceHandler");
		ISFSObject data = new SFSObject();	
		String log = dt.getUtfString("logDebug"); 	
		int chooseDice = dt.getInt("Choose");
		//trace("Client gui len 2019: " + log);
		Random rd = new Random();
		int rd1 = rd.nextInt(5);
		//trace("Gia tri ngau nhien:" + rd1);
		
			if(chooseDice != 6)
			{
				//trace("Mat xuc xac nguoi choi chon: " + chooseDice);
				if(chooseDice == rd1)
				{
					DebugClientHandler.gold += 10;
				}
				else {
					DebugClientHandler.gold -= 10;
				}		
			}
			else
			{
				//trace("chooseDice is null");
			}
		data.putUtfString("mess","server gui du lieu cho client" );
		data.putLong("gold", DebugClientHandler.gold);
		data.putInt("facedice", rd1);			
		this.send("Thongbaoserver", data, u);
		//trace("gui du lieu xuong client");
	}
}
