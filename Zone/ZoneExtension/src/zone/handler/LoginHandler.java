package zone.handler;

import zone.utils.*;
import com.smartfoxserver.v2.core.ISFSEvent;
import com.smartfoxserver.v2.core.SFSEventParam;
import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.exceptions.SFSException;
import com.smartfoxserver.v2.exceptions.SFSLoginException;
import com.smartfoxserver.v2.extensions.BaseServerEventHandler;

public class LoginHandler extends BaseServerEventHandler{
	@Override 
	public void handleServerEvent (ISFSEvent event) throws SFSException{
		trace("Command");
		String username = (String) event.getParameter(SFSEventParam.LOGIN_NAME);
		trace("username: "+ username);
		ISFSObject inData = (SFSObject) event.getParameter(SFSEventParam.LOGIN_IN_DATA);
		String command = inData.getUtfString(SF.COMMAND).trim();
		trace("Command: " + command);
		
		ISFSObject outData = (SFSObject) event.getParameter(SFSEventParam.LOGIN_OUT_DATA);
		
		
		
		//Lay Pass client gui len
		String password = inData.getUtfString("pawod").trim();
		//lay phong hien tai dang dang nhap tren server
		String currentRoom = null;
		//lay ten user dang nhap tren server bang getApi() cua smartfox
		//biến user của smartfox chứa các thuộc tính trên server khi đăng nhập vs tên người dùng
		//user được tạo ra khi người dùng đăng nhập thành công và server sẽ tạo ra biến User
		User user = getApi().getUserByName(username);
		
		if(user != null)
		{
					
			Room currentroom = user.getLastJoinedRoom();
			if(currentroom != null)
			{
				currentRoom = currentroom.getName();
			}
			getApi().disconnectUser(user);
		}
		long mobile = 0L;
		if(command.equals(SF.REGISTER))
		{
			mobile = inData.getLong(SF.MOBILE);
		}
		if(username == null || "".equals(username) || password == null || equals(password))
		{
			throw new SFSLoginException("Loi Dang Nhap");
		}
		if(SF.LOGIN.equals(command)) {
			boolean success = process(command, currentRoom, username, password, mobile, outData);
			if(!success) {
				throw new SFSLoginException("Error Login");
			}
		}
	}
		private boolean process(String command, String currentRoom, String username, String password, long mobile, ISFSObject outData)
		{
			try
			{
				trace("Dang Nhap Thanh Cong");
				outData.putUtfString(SF.USERNAME, username);
				outData.putUtfString(SF.MESS, "Dang Nhap Thanh Cong 123");
			}
			catch (Throwable e) {
				trace("Error while processing login");
			}
			return true;
		}
		
	
	
}
