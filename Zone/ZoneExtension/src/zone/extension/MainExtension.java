package zone.extension;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;
import java.util.concurrent.TimeUnit;
import java.lang.*;

import com.smartfoxserver.v2.SmartFoxServer;
import com.smartfoxserver.v2.core.ISFSEvent;
import com.smartfoxserver.v2.core.SFSEventType;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.exceptions.SFSException;
import com.smartfoxserver.v2.extensions.BaseServerEventHandler;
import com.smartfoxserver.v2.extensions.SFSExtension;

import zone.data.*;
import zone.handler.ChooseDiceHandler;
import zone.handler.DebugClientHandler;
import zone.handler.LoginHandler;
import zone.handler.UserExitHandler;
public class MainExtension extends SFSExtension {
	private World world;
	public World getWorld()
	{
		return world;
	}
	@Override
	public void init() {
		addEventHandler(SFSEventType.SERVER_READY, new BaseServerEventHandler() {
			
			@Override
			public void handleServerEvent(ISFSEvent arg0) throws SFSException {
				// TODO Auto-generated method stub
					_customInit();
			}
		});
	}
	private void _customInit() {
		// TODO Auto-generated method stub
		world = new World(this);
		addEventHandler(SFSEventType.USER_LOGIN, new LoginHandler());
		addRequestHandler("DebugClient", new DebugClientHandler());
		addRequestHandler("ChooseDice", new ChooseDiceHandler());
	}
	
}
