﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncClientServer.Helper;

namespace AsyncClientServer.Model.State
{
	public class FileReceivedState : ClientState
	{
		public FileReceivedState(IAsyncClient client) : base(client)
		{
		}

		public FileReceivedState(IAsyncSocketListener server) : base(server)
		{
		}

		public override void Receive(IStateObject state, int receive)
		{
			if (Client != null)
			{
				Client.InvokeAndReset(state);
				Client.ChangeState(new InitReceiveState(Client));
				Client.CState.Receive(state, receive);
				return;
			}

			if (Server != null)
			{
				Server.InvokeAndReset(state);
				Server.CurrentState = new InitReceiveState(Server);
				Server.CurrentState.Receive(state, receive);
			}


		}
	}
}