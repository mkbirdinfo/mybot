// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
using Microsoft.Bot.Builder.AI.Luis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace EchoBot
{
    /// <summary>
    /// Represents a bot that processes incoming activities.
    /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
    /// This is a Transient lifetime service.  Transient lifetime services are created
    /// each time they're requested. For each Activity received, a new instance of this
    /// class is created. Objects that are expensive to construct, or have a lifetime
    /// beyond the single turn, should be carefully managed.
    /// For example, the <see cref="MemoryStorage"/> object and associated
    /// <see cref="IStatePropertyAccessor{T}"/> object are created with a singleton lifetime.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
    public class EchoBotBot : IBot
    {
        private readonly EchoBotAccessors _accessors;
        private readonly ILogger _logger;
        private LuisRecognizer Recognizer { get; } = null;
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="conversationState">The managed conversation state.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory"/> that is hooked to the Azure App Service provider.</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1#windows-eventlog-provider"/>
        public EchoBotBot(ConversationState conversationState, ILoggerFactory loggerFactory, LuisRecognizer luis)
        {
            if (conversationState == null)
            {
                throw new System.ArgumentNullException(nameof(conversationState));
            }

            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }
            this.Recognizer=luis?? throw new System.ArgumentNullException(nameof(luis));
            _accessors = new EchoBotAccessors(conversationState)
            {
                CounterState = conversationState.CreateProperty<CounterState>(EchoBotAccessors.CounterStateName),
            };

            _logger = loggerFactory.CreateLogger<EchoBotBot>();
            _logger.LogTrace("Turn start.");
        }

        /// <summary>
        /// Every conversation turn for our Echo Bot will call this method.
        /// There are no dialogs used, since it's "single turn" processing, meaning a single
        /// request and response.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        /// <seealso cref="BotStateSet"/>
        /// <seealso cref="ConversationState"/>
        /// <seealso cref="IMiddleware"/>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types

            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                var recognizerResult = await this.Recognizer.RecognizeAsync(turnContext, cancellationToken);
                var topIntent = recognizerResult?.GetTopScoringIntent();
                string strIntent = (topIntent != null) ? topIntent.Value.intent : "";
                double dblIntentScore = (topIntent != null) ? topIntent.Value.score : 0.0;
                // Get the conversation state from the turn context.
                //  var state = await _accessors.CounterState.GetAsync(turnContext, () => new CounterState());

                // Bump the turn count for this conversation.
                //  state.TurnCount++;
     
                    // Set the property using the accessor.
                    //await _accessors.CounterState.SetAsync(turnContext, state);

                // Save the new turn count into the conversation state.
                //   await _accessors.ConversationState.SaveChangesAsync(turnContext);

                // Echo back to the user whatever they typed.
                //  var responseMessage = $"Turn {state.TurnCount}: You sent {turnContext.Activity.Text} which has length of {turnContext.Activity.Text.Length} Characters\n";
                // await turnContext.SendActivityAsync(responseMessage);
                if (strIntent != "")
                {
                    switch (strIntent)
                    {
                        case "None":
                            await turnContext.SendActivityAsync("Sorry, I don't understand.");
                            break;
                        case "Welcome":
                            await turnContext.SendActivityAsync("Hey, how can i help you");
                            break;


                        case "Searchflight":
                            var msg = FlightSearch.FlightSearchResult(recognizerResult.Entities);
                            await turnContext.SendActivityAsync(msg);
                            break;
                        case "Hotel":
                            var msg1 = Hotel.Hotelsearch(recognizerResult.Entities);
                            await turnContext.SendActivityAsync(msg1);
                            break;
                        case "Booking":
                            var msg2 =Booking.GiveFeedBack(recognizerResult.Entities);     // Hotel.Hotelsearch(recognizerResult.Entities);
                            await turnContext.SendActivityAsync(msg2);
                            break;
                        default:
                            // Received an intent we didn't expect, so send its name and score.
                            await turnContext.SendActivityAsync(
                                $"Intent: {topIntent.Value.intent} ({topIntent.Value.score}). Your search matching to flight");
                            break;
                    }
                }
            }
            else if(turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
            {
                await turnContext.SendActivityAsync($"Hi , i am techvai");
            }
        }
    }
}