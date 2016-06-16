A shameless stealing of idea from https://storebot.me/bot/whosinbot (done by  my fellow ThoughtWorker Col Harris). It's ported to use Microsoft Bot Framework

## Supported Chat Platforms
- Telegram (@AnotherAttendanceBot)

## Supported Commands

<table>
    <tr>
        <th>Command</th>
        <th>Description</th>
    </tr>
    <tr>
        <td>
            /start EVENT_NAME
        </td>
        <td>
            Start an event. When a new event is started, it's automatically set as current event
        </td>
    </tr>
    <tr>
        <td>
            /status
            <br/>
            /status EVENT_NAME
        </td>
        <td>
            View current status of an event (how many Yes/No/Maybe). When calling without parameter, it's default to current event
        </td>
    </tr>
    <tr>
        <td>
            /in
        </td>
        <td>
            Set In for current user
        </td>
    </tr>
    <tr>
        <td>
            /out (Message)
        </td>
        <td>
            Set Out for current user with an optional message
        </td>
    </tr>
    <tr>
        <td>
            /maybe (Message)
        </td>
        <td>
            Set Maybe for current user with an optional message
        </td>
    </tr>
    <tr>
        <td>
            /list
        </td>
        <td>
            List all events available
        </td>
    </tr>
</table>