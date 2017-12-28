// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Game.Beatmaps;
using osu.Game.Online.Multiplayer;
using osu.Game.Rulesets;
using osu.Game.Screens.Multi;
using osu.Game.Users;

namespace osu.Game.Tests.Visual
{
    public class TestCaseMultiplayerScreens : OsuTestCase
    {
        [BackgroundDependencyLoader]
        private void load(RulesetStore rulesets)
        {
            Multiplayer multi;
            Add(multi = new Multiplayer());

            multi.Rooms = new[]
            {
                new Room
                {
                    Name = { Value = @"My Awesome Room" },
                    Host = { Value = new User { Username = @"flyte", Id = 3103765, Country = new Country { FlagName = @"JP" } } },
                    Status = { Value = new RoomStatusOpen() },
                    Type = { Value = new GameTypeTeamVersus() },
                    Beatmap =
                    {
                        Value = new BeatmapInfo
                        {
                            StarDifficulty = 5.2,
                            Ruleset = rulesets.GetRuleset(0),
                            Metadata = new BeatmapMetadata
                            {
                                Title = @"Platina",
                                Artist = @"Maaya Sakamoto",
                                AuthorString = @"uwutm8",
                            },
                            BeatmapSet = new BeatmapSetInfo
                            {
                                OnlineInfo = new BeatmapSetOnlineInfo
                                {
                                    Covers = new BeatmapSetOnlineCovers
                                    {
                                        Cover = @"https://assets.ppy.sh/beatmaps/560573/covers/cover.jpg?1492722343",
                                    },
                                },
                            },
                        }
                    },
                    MaxParticipants = { Value = 200 },
                    Participants =
                    {
                        Value = new[]
                        {
                            new User { Username = @"flyte", Id = 3103765, GlobalRank = 1425 },
                            new User { Username = @"Cookiezi", Id = 124493, GlobalRank = 5466 },
                            new User { Username = @"Angelsim", Id = 1777162, GlobalRank = 2873 },
                            new User { Username = @"Rafis", Id = 2558286, GlobalRank = 4687 },
                            new User { Username = @"hvick225", Id = 50265, GlobalRank = 3258 },
                            new User { Username = @"peppy", Id = 2, GlobalRank = 6251 },
                        },
                    },
                },
                new Room
                {
                    Name = { Value = @"A Better Room Than The Above" },
                    Host = { Value = new User { Username = @"peppy", Id = 2, Country = new Country { FlagName = @"AU" } } },
                    Status = { Value = new RoomStatusPlaying() },
                    Type = { Value = new GameTypeTeamVersus() },
                    Beatmap =
                    {
                        Value = new BeatmapInfo
                        {
                            StarDifficulty = 5.2,
                            Ruleset = rulesets.GetRuleset(0),
                            Metadata = new BeatmapMetadata
                            {
                                Title = @"Yoru no Kodomo tachi",
                                Artist = @"Roses Epicurean",
                                AuthorString = @"Katyusha",
                            },
                            BeatmapSet = new BeatmapSetInfo
                            {
                                OnlineInfo = new BeatmapSetOnlineInfo
                                {
                                    Covers = new BeatmapSetOnlineCovers
                                    {
                                        Cover = @"https://assets.ppy.sh//beatmaps/325112/covers/cover.jpg?1456483843",
                                    },
                                },
                            },
                        }
                    },
                    MaxParticipants = { Value = 10 },
                    Participants =
                    {
                        Value = new[]
                        {
                            new User { Username = @"flyte", Id = 3103765, GlobalRank = 1425 },
                            new User { Username = @"Tom94", Id = 1857058, GlobalRank = 3847 },
                            new User { Username = @"peppy", Id = 2, GlobalRank = 6251 },
                        },
                    },
                },
                new Room
                {
                    Name = { Value = @"The Best Room" },
                    Host = { Value = new User { Username = @"DrabWeb", Id = 6946022, Country = new Country { FlagName = @"CA" } } },
                    Status = { Value = new RoomStatusPlaying() },
                    Type = { Value = new GameTypeTeamVersus() },
                    Beatmap =
                    {
                        Value = new BeatmapInfo
                        {
                            StarDifficulty = 5.2,
                            Ruleset = rulesets.GetRuleset(3),
                            Metadata = new BeatmapMetadata
                            {
                                Title = @"HAELEQUIN",
                                Artist = @"orangentle / Yu_Asahina",
                                AuthorString = @"imariL",
                            },
                            BeatmapSet = new BeatmapSetInfo
                            {
                                OnlineInfo = new BeatmapSetOnlineInfo
                                {
                                    Covers = new BeatmapSetOnlineCovers
                                    {
                                        Cover = @"https://assets.ppy.sh//beatmaps/174550/covers/cover.jpg?1456489737",
                                    },
                                },
                            },
                        }
                    },
                    MaxParticipants = { Value = 5 },
                    Participants =
                    {
                        Value = new[]
                        {
                            new User { Username = @"DrabWeb", Id = 6946022, GlobalRank = 11391 },
                            new User { Username = @"peppy", Id = 2, GlobalRank = 6251 },
                            new User { Username = @"flyte", Id = 3103765, GlobalRank = 1425 },
                            new User { Username = @"Rafis", Id = 2558286, GlobalRank = 4687 },
                            new User { Username = @"hvick225", Id = 50265, GlobalRank = 3258 },
                        },
                    },
                },
            };
        }
    }
}
