#region License
// Copyright (c) 2014 Bit Rave Pty Ltd
//
// 1. OWNERSHIP, LICENSE GRANT
// Subject to the terms below (the 'License Agreement'), Bit Rave Pty Ltd ('We', 'Us') 
// grants you to install and use Azure Mobile Services for Unity (the 'Software').
//
// This is a license agreement and not an agreement for sale. We reserve ownership 
// of all intellectual property rights inherent in or relating to the Software, 
// which include, but are not limited to, all copyright, patent rights, all rights 
// in relation to registered and unregistered trademarks (including service marks), 
// confidential information (including trade secrets and know-how) and all rights 
// other than those expressly granted by this Agreement.
//
// Subject to the terms and conditions of this License Agreement, We grant to You 
// a non-transferable, non-exclusive license for a Designated User (as defined below) 
// within Your organization to install and use the Software on any workstations used 
// exclusively by such Designated User and for You to distribute the Software as part 
// of your Unity applications or games, solely in connection with distribution of 
// the Software in accordance with sections 3 and 4 below. This license is not 
// sublicensable except as explicitly set forth herein. "Designated User(s)" shall 
// mean Your employee(s) acting within the scope of their employment or Your consultant(s) 
// or contractor(s) acting within the scope of the services they provide for You or on Your behalf.

// 2. PERMITTED USES, SOURCE CODE, MODIFICATIONS
// We provide You with source code so that You can create Modifications of the original Software, 
// where Modification means: a) any addition to or deletion from the contents of a file included 
// in the original Software or previous Modifications created by You, or b) any new file that 
// contains any part of the original Software or previous Modifications. While You retain all 
// rights to any original work authored by You as part of the Modifications, We continue to own 
// all copyright and other intellectual property rights in the Software.

// 3. DISTRIBUTION
// You may distribute the Software in any applications, frameworks, or elements (collectively 
// referred to as "Applications") that you develop using the Software in accordance with this 
// License Agreement, provided that such distribution does not violate the restrictions set 
// forth in section 4 of this agreement.

// You will not owe Us any royalties for Your distribution of the Software in accordance with 
// this License Agreement.

// 4. PROHIBITED USES
// You may not redistribute the Software or Modifications other than by including the Software 
// or a portion thereof within Your own product, which must have substantially different 
// functionality than the Software or Modifications and must not allow any third party to use 
// the Software or Modifications, or any portions thereof, for software development or application 
// development purposes. You are explicitly not allowed to redistribute the Software or 
// Modifications as part of any product that can be described as a development toolkit or library 
// or is intended for use by software developers or application developers and not end-users.

// 5. TERMINATION
// This Agreement shall terminate automatically if you fail to comply with the limitations 
// described in this Agreement. No notice shall be required to effectuate such termination. 
// Upon termination, you must remove and destroy all copies of the Software. 

// 6. DISCLAIMER OF WARRANTY
// YOU AGREE THAT WE HAVE MADE NO EXPRESS WARRANTIES, ORAL OR WRITTEN, TO YOU REGARDING THE 
// SOFTWARE AND THAT THE SOFTWARE IS BEING PROVIDED TO YOU 'AS IS' WITHOUT WARRANTY OF ANY KIND.
//  WE DISCLAIM ANY AND ALL OTHER WARRANTIES, WHETHER EXPRESSED, IMPLIED, OR STATUTORY. YOUR RIGHTS
//  MAY VARY DEPENDING ON THE STATE IN WHICH YOU LIVE. WE SHALL NOT BE LIABLE FOR INDIRECT, 
// INCIDENTAL, SPECIAL, COVER, RELIANCE, OR CONSEQUENTIAL DAMAGES RESULTING FROM THE USE OF THIS PRODUCT.

// 7. LIMITATION OF LIABILITY
// YOU USE THIS PROGRAM SOLELY AT YOUR OWN RISK. IN NO EVENT SHALL WE BE LIABLE TO YOU FOR ANY DAMAGES,
// INCLUDING BUT NOT LIMITED TO ANY LOSS, OR OTHER INCIDENTAL, INDIRECT OR CONSEQUENTIAL DAMAGES OF 
// ANY KIND ARISING OUT OF THE USE OF THE SOFTWARE, EVEN IF WE HAVE BEEN ADVISED OF THE POSSIBILITY OF
// SUCH DAMAGES. IN NO EVENT WILL WE BE LIABLE FOR ANY CLAIM, WHETHER IN CONTRACT, TORT, OR ANY OTHER
// THEORY OF LIABILITY, EXCEED THE COST OF THE SOFTWARE. THIS LIMITATION SHALL APPLY TO CLAIMS OF 
// PERSONAL INJURY TO THE EXTENT PERMITTED BY LAW.

// 8. MISCELLANEOUS
// The license granted herein applies only to the version of the Software available when acquired
// in connection with the terms of this Agreement. Any previous or subsequent license granted to
// You for use of the Software shall be governed by the terms and conditions of the agreement entered
// in connection with the acquisition of that version of the Software. You agree that you will comply
// with all applicable laws and regulations with respect to the Software, including without limitation
// all export and re-export control laws and regulations.
using System;


#endregion


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bitrave.Azure;

public class AzureUILeaderboard : MonoBehaviour 
{
	private AzureMobileServices azure;

	[SerializeField]
	public string AzureEndPoint = "https://deadlyfingers.azure-mobile.net/"; // Your Connection URL
	
	[SerializeField]
	public string ApplicationKey = "NKnKzIEoPQpsQfMdqxTdQTvdMNaxHF20"; // Your API Key

	// Table items
	public List<Leaderboard> _leaderboardItems = new List<Leaderboard>();
	
	// Use this for initialization
	void Start () 
	{
        azure = new AzureMobileServices(AzureEndPoint, ApplicationKey);
    }

	// Update is called once per frame
	void Update () 
	{

	}

	// Interface
	private int width = 44;
	private int height = 44;
	private int colWidth = 200;

	// Item to insert
	private Leaderboard _leaderboard = new Leaderboard()
	{
		Score = 0,
		Username = "Anon"
	};

	// Item to update
	private Leaderboard _selectedItem = new Leaderboard()
	{
		Score = 0,
		Username = "Anon"
	};

	private Vector2 scrollPosition;
	
	public void OnGUI()
    {
        GUILayout.BeginVertical();        
        GUILayout.BeginHorizontal();

		// Column 1
		GUILayout.BeginVertical(GUILayout.Width(colWidth));
		GUILayout.Label("Azure End Point");
		AzureEndPoint = GUILayout.TextField(AzureEndPoint, GUILayout.Width(colWidth));
		GUILayout.Label("Application Key");
		ApplicationKey = GUILayout.TextField(ApplicationKey, GUILayout.Width(colWidth));
		GUILayout.EndVertical();

		// hide rest of GUI if no connection available
		GUI.enabled = ( azure != null ); 

		// Column 2
		GUILayout.BeginVertical(GUILayout.Width(colWidth));
		GUILayout.Label("Username");
		_leaderboard.Username = GUILayout.TextField(_leaderboard.Username);
		GUILayout.Label("Score");
		_leaderboard.Score = Convert.ToInt32( GUILayout.TextField(""+_leaderboard.Score) );
		if(GUILayout.Button("Add Score", GUILayout.MinWidth(width), GUILayout.Height(height) ))
        {
            // Note: You don't need to do the following, it's done in the insert method. 
            // _leaderboard.Id = null;

			if(_leaderboard.Score > 0) 
			{
				// only insert score if greater than 0
				azure.Insert<Leaderboard>(_leaderboard);
			}
			else
			{
				Debug.Log("Score must be > 0 to insert");
			}
        }
		GUILayout.Label("Id: " + _leaderboard.Id); // unique item Id (returned once submitted succesfully)
        GUILayout.EndVertical();

		// Column 3
		GUILayout.BeginVertical(GUILayout.Width(colWidth));
		if (GUILayout.Button("Query User Scores", GUILayout.MinWidth(width), GUILayout.Height(height) ))
        {
            _leaderboardItems.Clear();
			// get a user's scores
			azure.Where<Leaderboard>( p => p.Username == _leaderboard.Username, ReadHandler); 
        }
		if (GUILayout.Button("List All Scores",  GUILayout.MinWidth(width), GUILayout.Height(height) ))
        {
            GetAllItems();
        }
		if (GUILayout.Button("List Scores 500+",  GUILayout.MinWidth(width), GUILayout.Height(height) ))
		{
			_leaderboardItems.Clear();
			// get high scores
			azure.Where<Leaderboard>( p => p.Score >= 500, ReadHandler); 
		}
        GUILayout.Label("Item count: " + _leaderboardItems.Count);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Height(300));
        
		// Column 4
        GUILayout.BeginVertical();
        foreach (var item in _leaderboardItems)
        {
            GUILayout.BeginHorizontal();
			if (GUILayout.Button(">", GUILayout.Width(width), GUILayout.Height(height) ))
            {
                _selectedItem = item;
            }
			GUILayout.Label(item.Username);
			GUILayout.Label(Convert.ToString(item.Score));
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        
        GUILayout.EndScrollView();
        GUILayout.EndVertical();

        GUILayout.BeginVertical(GUILayout.Width(colWidth));

        var was = GUI.enabled;

        GUI.enabled = _selectedItem.Id != null;

        GUILayout.Label("Id: " + _selectedItem.Id);
		if(_selectedItem.Score > 0)
		{
			_selectedItem.Score = Convert.ToInt32( GUILayout.TextField(""+_selectedItem.Score) );
		}
		_selectedItem.Username = GUILayout.TextField(_selectedItem.Username);

		if (GUILayout.Button("Update", GUILayout.MinWidth(width), GUILayout.Height(height) ))
        {
            azure.Update<Leaderboard>(_selectedItem);
        }
		if (GUILayout.Button("Delete", GUILayout.MinWidth(width), GUILayout.Height(height) ))
        {
			azure.Delete<Leaderboard>(_selectedItem);
        }

        GUI.enabled = was;

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUI.enabled = true;
    }

	public void GetAllItems()
	{
		_leaderboardItems.Clear();
		azure.Where<Leaderboard>(p => p.Username != null, ReadHandler);
	}
	
	public void ReadHandler(AzureResponse<List<Leaderboard>> response)
	{
		
		var list = response.ResponseData;
		
		Debug.Log("Items ==================");
		foreach (var item in list)
		{
			Debug.Log( Convert.ToString(item.Score) + "," + item.Username + "," + item.Id);
			_leaderboardItems.Add(item);
		}
		Debug.Log("==================");
	}

}

