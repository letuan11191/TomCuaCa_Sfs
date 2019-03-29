package zone.handler;

import java.util.Random;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;


public class UserExitHandler extends BaseClientRequestHandler {

	@Override
	public void handleClientRequest (User u, ISFSObject dt) {
		trace("Start UserExitHandler");
	}

}
