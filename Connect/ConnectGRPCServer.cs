﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using LiveSplit.Model;
using LiveSplit.Model.Comparisons;

namespace LiveSplit.Connect
{
    public static class ComparisonMethodExtension
    {
        public static string ComparisonName(this ComparisonMethod cm)
        {
            switch (cm)
            {
                case ComparisonMethod.PersonalBest:
                    return LiveSplit.Model.Run.PersonalBestComparisonName;

                case ComparisonMethod.BestSegment:
                    return BestSegmentsComparisonGenerator.ComparisonName;

                case ComparisonMethod.BestSplitTimes:
                    return BestSplitTimesComparisonGenerator.ComparisonName;

                case ComparisonMethod.AverageSegments:
                    return AverageSegmentsComparisonGenerator.ComparisonName;

                case ComparisonMethod.BalancedPb:
                    return PercentileComparisonGenerator.ComparisonName;

                default:
                    return null;
            }
        }

        public static ComparisonMethod FromString(string cm)
        {
            switch (cm)
            {
                case LiveSplit.Model.Run.PersonalBestComparisonName:
                    return ComparisonMethod.PersonalBest;

                case BestSegmentsComparisonGenerator.ComparisonName:
                    return ComparisonMethod.BestSegment;

                case BestSplitTimesComparisonGenerator.ComparisonName:
                    return ComparisonMethod.BestSplitTimes;

                case AverageSegmentsComparisonGenerator.ComparisonName:
                    return ComparisonMethod.AverageSegments;

                case PercentileComparisonGenerator.ComparisonName:
                    return ComparisonMethod.BalancedPb;

                default:
                    return ComparisonMethod.Undefined;
            }
        }
    }

    public static class TimeExtension
    {
        public static Duration ToDuration(this TimeSpan? timespan)
        {
            if (timespan == null)
            {
                return null;
            }
            return Duration.FromTimeSpan((TimeSpan)timespan);
        }

        public static LiveSplit.Connect.Time ToConnectTime(this TimeSpan? timespan, TimingMethod timingMethod)
        {
            var time = new Time();
            var duration = timespan.ToDuration();
            switch (timingMethod)
            {
                case TimingMethod.Undefined:
                    return null;

                case TimingMethod.RealTime:
                    time.RealTime = duration;
                    break;

                case TimingMethod.GameTime:
                    time.GameTime = duration;
                    break;
            }
            return time;
        }

        public static LiveSplit.Connect.Time ToConnectTime(this TimeSpan? timespan, LiveSplit.Model.TimingMethod timingMethod)
        {
            return timespan.ToConnectTime(timingMethod.ToConnectTimingMethod());
        }

        public static LiveSplit.Connect.Time ToConnectTime(this LiveSplit.Model.Time livesplitTime)
        {
            Time time = new Time();
            time.RealTime = livesplitTime.RealTime.ToDuration();
            time.GameTime = livesplitTime.GameTime.ToDuration();
            return time;
        }
    }

    public static class TimerPhaseExtension
    {
        public static TimerPhase ToConnectTimerPhase(this LiveSplit.Model.TimerPhase phase)
        {
            switch (phase)
            {
                case Model.TimerPhase.NotRunning:
                    return TimerPhase.NotRunning;

                case Model.TimerPhase.Running:
                    return TimerPhase.Running;

                case Model.TimerPhase.Ended:
                    return TimerPhase.Ended;

                case Model.TimerPhase.Paused:
                    return TimerPhase.Paused;

                default:
                    return TimerPhase.Undefined;
            }
        }
    }

    public static class TimingMethodExtension
    {
        public static LiveSplit.Model.TimingMethod ToLiveSplitTimingMethod(this TimingMethod method)
        {
            switch (method)
            {
                case TimingMethod.RealTime:
                    return LiveSplit.Model.TimingMethod.RealTime;

                case TimingMethod.GameTime:
                    return LiveSplit.Model.TimingMethod.GameTime;

                default:
                    throw new Exception("Can't cast Connect TimingMethod to LiveSplit TimingMethod");
            }
        }

        public static TimingMethod ToConnectTimingMethod(this LiveSplit.Model.TimingMethod method)
        {
            switch (method)
            {
                case LiveSplit.Model.TimingMethod.RealTime:
                    return TimingMethod.RealTime;

                case LiveSplit.Model.TimingMethod.GameTime:
                    return TimingMethod.GameTime;
            }
            return TimingMethod.Undefined;
        }
    }

    public static class SegmentExtension
    {
        public static Segment ToConnectSegment(this LiveSplit.Model.ISegment s)
        {
            return new Segment
            {
                Name = s.Name,
                SplitTime = s.SplitTime.ToConnectTime(),
                PersonalBestSplitTime = s.PersonalBestSplitTime.ToConnectTime(),
                BestSegmentTime = s.BestSegmentTime.ToConnectTime(),
            };
        }
    }

    public static class RunExtension
    {
        public static Run ToConnectRun(this LiveSplit.Model.IRun run)
        {
            return new Run
            {
                AttemptCount = (uint)run.AttemptCount,
                GameName = run.GameName,
                Platform = run.Metadata.PlatformName,
                Region = run.Metadata.RegionName,
                RunCategory = run.CategoryName,
                UsesEmulator = run.Metadata.UsesEmulator
            };
        }
    }

    public class ConnectGRPCServer : Server.ServerBase
    {
        private TimerModel Model { get; set; }
        private LiveSplitState State { get; set; }

        public ConnectGRPCServer(LiveSplitState state)
        {
            State = state;
            Model = new TimerModel
            {
                CurrentState = State
            };
        }

        public override Task<StartOrSplitResponse> StartOrSplit(StartOrSplitRequest request, ServerCallContext context)
        {
            if (State.CurrentPhase == LiveSplit.Model.TimerPhase.Running)
            {
                Model.Split();
            }
            else
            {
                Model.Start();
            }
            return Task.FromResult(new StartOrSplitResponse());
        }

        public override Task<SkipSplitResponse> SkipSplit(SkipSplitRequest request, ServerCallContext context)
        {
            Model.SkipSplit();
            return Task.FromResult(new SkipSplitResponse());
        }

        public override Task<UnSplitResponse> UnSplit(UnSplitRequest request, ServerCallContext context)
        {
            Model.UndoSplit();
            return Task.FromResult(new UnSplitResponse());
        }

        public override Task<PauseResponse> Pause(PauseRequest request, ServerCallContext context)
        {
            Model.Pause();
            return Task.FromResult(new PauseResponse());
        }

        public override Task<ResetResponse> Reset(ResetRequest request, ServerCallContext context)
        {
            Model.Reset();
            return Task.FromResult(new ResetResponse());
        }

        public override Task<GetTimeResponse> GetTime(GetTimeRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetTimeResponse { Time = State.CurrentTime.ToConnectTime() });
        }

        public override Task<GetCurrentSegmentResponse> GetCurrentSegment(GetCurrentSegmentRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetCurrentSegmentResponse
            {
                Segment = Model.CurrentState.CurrentSplit?.ToConnectSegment(),
            });
        }

        public override Task<FindSegmentResponse> FindSegment(FindSegmentRequest request, ServerCallContext context)
        {
            Segment segment = null;
            foreach (var s in Model.CurrentState.Run)
            {
                if (s.Name.ToLower() == request.SegmentName.ToLower())
                {
                    segment = s.ToConnectSegment();
                    break;
                }
            }
            return Task.FromResult(new FindSegmentResponse { Segment = segment });
        }

        public override Task<ListSegmentResponse> ListSegment(ListSegmentRequest request, ServerCallContext context)
        {
            var response = new ListSegmentResponse();
            foreach (var s in Model.CurrentState.Run)
            {
                response.Segments.Add(s.ToConnectSegment());
            }

            return Task.FromResult(response);
        }

        public override Task<GetRunResponse> GetRun(GetRunRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetRunResponse { Run = Model.CurrentState.Run.ToConnectRun() });
        }

        public override Task<GetCurrentComparisonMethodResponse> GetCurrentComparisonMethod(GetCurrentComparisonMethodRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetCurrentComparisonMethodResponse
            {
                Method = ComparisonMethodExtension.FromString(Model.CurrentState.CurrentComparison)
            });
        }

        public override Task<GetCurrentTimerPhaseResponse> GetCurrentTimerPhase(GetCurrentTimerPhaseRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetCurrentTimerPhaseResponse
            {
                Phase = Model.CurrentState.CurrentPhase.ToConnectTimerPhase(),
            });
        }

        public override Task<GetCurrentTimingMethodResponse> GetCurrentTimingMethod(GetCurrentTimingMethodRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetCurrentTimingMethodResponse
            {
                Method = Model.CurrentState.CurrentTimingMethod.ToConnectTimingMethod(),
            });
        }

        public override async Task WatchTime(WatchTimeRequest request, IServerStreamWriter<WatchTimeResponse> responseStream, ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(new WatchTimeResponse { CurrentTime = Model.CurrentState.CurrentTime.ToConnectTime() });
                await Task.Delay(request.RefreshRate.ToTimeSpan());
            }
        }

        public override async Task WatchTimerPhase(WatchTimerPhaseRequest request, IServerStreamWriter<WatchTimerPhaseResponse> responseStream, ServerCallContext context)
        {
            async void timerPhaseHandler(object sender, EventArgs evt)
            {
                await responseStream.WriteAsync(new WatchTimerPhaseResponse
                {
                    Phase = Model.CurrentState.CurrentPhase.ToConnectTimerPhase(),
                });
            }
            async void timerPhaseHandlerReset(object sender, LiveSplit.Model.TimerPhase phase)
            {
                await responseStream.WriteAsync(new WatchTimerPhaseResponse
                {
                    Phase = Model.CurrentState.CurrentPhase.ToConnectTimerPhase(),
                });
            }

            Model.CurrentState.OnPause += timerPhaseHandler;
            Model.CurrentState.OnResume += timerPhaseHandler;
            Model.CurrentState.OnUndoAllPauses += timerPhaseHandler;
            Model.CurrentState.OnReset += timerPhaseHandlerReset;
            Model.CurrentState.OnStart += timerPhaseHandler;

            void unregisterHandlers()
            {
                Model.CurrentState.OnPause -= timerPhaseHandler;
                Model.CurrentState.OnResume -= timerPhaseHandler;
                Model.CurrentState.OnUndoAllPauses -= timerPhaseHandler;
                Model.CurrentState.OnReset -= timerPhaseHandlerReset;
                Model.CurrentState.OnStart -= timerPhaseHandler;
            }
            context.CancellationToken.Register(unregisterHandlers);

            await context.CancellationToken;
        }

        public override async Task WatchSplit(WatchSplitRequest request, IServerStreamWriter<WatchSplitResponse> responseStream, ServerCallContext context)
        {
            async void splitHandler(object sender, EventArgs evt)
            {
                // Skip first split, it's start of the run
                if (Model.CurrentState.CurrentSplitIndex < 1) return;

                await responseStream.WriteAsync(new WatchSplitResponse
                {
                    Segment = Model.CurrentState.Run[Model.CurrentState.CurrentSplitIndex - 1].ToConnectSegment()
                });
            }
            Model.CurrentState.OnSplit += splitHandler;

            void unregisterHandlers()
            {
                Model.CurrentState.OnSplit -= splitHandler;
            }
            context.CancellationToken.Register(unregisterHandlers);

            await context.CancellationToken;
        }
    }
}
