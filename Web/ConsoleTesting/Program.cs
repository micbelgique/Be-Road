using Contracts.Dal;
using Contracts.Logic;
using Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTesting
{
    class Program
    {
        static BeContractCall CreateContractCall(string id, params string[] parameters)
        {
            var call = new BeContractCall()
            {
                Id = id,
                ISName = "Console App",
                Inputs = new Dictionary<string, dynamic>()
            };
            parameters.ToList().ForEach(param =>
            {
                var split = param.Split(':');
                if(split.Length == 2)
                {
                    if(int.TryParse(split[1], out int n))
                        call.Inputs.Add(split[0], n);
                    else
                        call.Inputs.Add(split[0], split[1]);
                }
            });
            return call;
        }

        #region jsonInfos
        static string jsonInfos = @"[
    {
                'Id': 16,
		'IsReliable': true,
		'Name': 'Insomnia Client',
		'Justification': 'Testing purpose',
		'Date': '2018-05-03T15:04:45.253'

    },
	{
                'Id': 18,
		'IsReliable': false,
		'Name': 'TestMyRide',
		'Justification': 'Testing1',
		'Date': '2018-05-03T15:09:16.973'

    },
	{
                'Id': 19,
		'IsReliable': false,
		'Name': 'TestMyRide',
		'Justification': 'Testing',
		'Date': '2018-05-03T15:09:45.94'

    },
	{
                'Id': 20,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-03T16:20:57.55'

    },
	{
                'Id': 24,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-03T16:29:23.203'

    },
	{
                'Id': 25,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-03T16:32:50.977'

    },
	{
                'Id': 26,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-03T16:33:09.643'

    },
	{
                'Id': 28,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-03T16:38:52.46'

    },
	{
                'Id': 50,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-04T10:39:26.963'

    },
	{
                'Id': 65,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-04T10:56:25.213'

    },
	{
                'Id': 66,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-04T10:56:52.477'

    },
	{
                'Id': 67,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-04T10:57:13.92'

    },
	{
                'Id': 68,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-04T10:58:08.987'

    },
	{
                'Id': 73,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-04T11:26:33.197'

    },
	{
                'Id': 74,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-04T11:27:25.743'

    },
	{
                'Id': 76,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-04T11:31:39.78'

    },
	{
                'Id': 90,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-04T15:19:02.813'

    },
	{
                'Id': 95,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T10:53:19.37'

    },
	{
                'Id': 115,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T10:55:18.817'

    },
	{
                'Id': 119,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T10:55:54.44'

    },
	{
                'Id': 137,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T10:56:42.94'

    },
	{
                'Id': 155,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T10:58:45.033'

    },
	{
                'Id': 175,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:02:26.03'

    },
	{
                'Id': 193,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:03:22.523'

    },
	{
                'Id': 197,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:12:06.763'

    },
	{
                'Id': 214,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:14:31.76'

    },
	{
                'Id': 219,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:17:39.267'

    },
	{
                'Id': 238,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:18:52.27'

    },
	{
                'Id': 241,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:24:29.153'

    },
	{
                'Id': 260,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:25:21.09'

    },
	{
                'Id': 278,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:26:32.03'

    },
	{
                'Id': 280,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:28:14.083'

    },
	{
                'Id': 299,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:40:52.003'

    },
	{
                'Id': 318,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:43:20.077'

    },
	{
                'Id': 336,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:43:49.297'

    },
	{
                'Id': 344,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:52:52.26'

    },
	{
                'Id': 362,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T11:53:27.417'

    },
	{
                'Id': 367,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T12:01:19.13'

    },
	{
                'Id': 371,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T12:01:48.907'

    },
	{
                'Id': 375,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T12:02:15.597'

    },
	{
                'Id': 379,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T12:04:10.483'

    },
	{
                'Id': 383,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T12:05:13.197'

    },
	{
                'Id': 387,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-08T12:05:55.773'

    },
	{
                'Id': 388,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T12:06:20.017'

    },
	{
                'Id': 393,
		'IsReliable': true,
		'Name': 'Public Service/93011150162',
		'Justification': 'Displaying user info',
		'Date': '2018-05-08T12:06:40.237'

    },
	{
                'Id': 416,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-08T12:09:31.043'

    },
	{
                'Id': 417,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-08T12:18:44.717'

    },
	{
                'Id': 418,
		'IsReliable': true,
		'Name': 'Privacy Passport',
		'Justification': 'Reading my own data',
		'Date': '2018-05-08T12:35:32.423'

    },
	{
                'Id': 419,
		'IsReliable': true,
		'Name': 'Insomnia Client',
		'Justification': 'Testing purpose',
		'Date': '2018-05-08T12:39:28.987'

    }]";
        #endregion

        class AccessInfoDto
        {
            public int Id { get; set; }
            public Boolean IsReliable { get; set; }
            public string Name { get; set; }
            public string Justification { get; set; }
            public DateTime Date { get; set; }
        }

        static void TestAccessInfos()
        {
            var accessInfos = JsonConvert.DeserializeObject<List<AccessInfoDto>>(jsonInfos);
            var grouped = accessInfos.GroupBy(ai => new
            {
                Date = ai.Date.ToShortDateString(),
                ai.IsReliable,
                ai.Justification,
                ai.Name
            }, ai => new
            {
                Date = ai.Date.ToShortDateString(),
                ai.IsReliable,
                ai.Justification,
                ai.Name,
                Total = accessInfos.Count(inside =>
                {
                    return inside.Date.ToShortDateString() == ai.Date.ToShortDateString()
                        && inside.IsReliable == inside.IsReliable
                        && inside.Justification == inside.Justification
                        && inside.Name == inside.Name;
                })
            })
            .Select(grp => grp.FirstOrDefault())
            .ToList();

            grouped.ForEach(access => Console.WriteLine(access));


        }

        static void Main(string[] args)
        {
            //Used for connection string
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());

            //var ctx = new ContractContext();
            //ctx.Contracts.Add(BeContractsMock.GetAddressByDogId());
            //ctx.Contracts.Add(BeContractsMock.GetAddressByOwnerId());
            //ctx.Contracts.Add(BeContractsMock.GetOwnerIdByDogId());
            //ctx.Contracts.Add(BeContractsMock.GetMathemathicFunction());
            //ctx.SaveChanges();
            //Console.WriteLine(ctx.Contracts.Count());
            var manager = new ContractManager()
            {
                AsService = new AdapterServerServiceImpl(),
                BcService = new BeContractServiceImpl(),
                AuthService = new AuthorisationServerServiceImpl()
            };

            manager.CallAsync(CreateContractCall("GetOwnerIdByDogId", "DogID:D-123")).Wait();
            manager.CallAsync(CreateContractCall("GetOwnerIdByDogId", "DogID:D-122")).Wait();
            manager.CallAsync(CreateContractCall("GetOwnerIdByDogId", "DogID:")).Wait();
            manager.CallAsync(CreateContractCall("GetAddressByOwnerId", "OwnerID:Mika !")).Wait();
            manager.CallAsync(CreateContractCall("GetAddressByDogId", "MyDogID:D-122")).Wait();
            manager.CallAsync(CreateContractCall("GetMathemathicFunction", "A:5", "B:19")).Wait();
            Console.ReadLine();
        }
    }
}
