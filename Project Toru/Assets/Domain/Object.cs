namespace Assets.Domain
{
	interface Object
	{
		string name
		{
			get;
			set;
		}

		string sprite
		{
			get;
			set;
		}

		Item[] items
		{
			get;
			set;
		}

		bool uncovered
		{
			get;
			set;
		}

		Dictionary<string, Delegate> options
		{
			get;
			set;
		}

        Object();

		public void draw();

		public Dictionary<string, Delegate> getOptions();
	}
}
