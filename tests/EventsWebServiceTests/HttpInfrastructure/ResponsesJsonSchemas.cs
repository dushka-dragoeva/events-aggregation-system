namespace EventsWebServiceTests.ApiInfrastructure
{
    internal static class ResponsesJsonSchemas
    {
        public static string EventResponseSchema() => @"{
                                                        'type': 'object',
                                                        'properties': {
                                                            'status': {
                                                                'type': 'string'
                                                            },
                                                            'time': {
                                                                'type': 'string'
                                                            },
                                                            'referenseId': {
                                                                'type': 'string'
                                                            }
                                                        },
                                                        'required': [
                                                          'status',
                                                          'time',
                                                          'referenseId'
                                                        ]
                                                    }";
    }
}
