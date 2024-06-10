namespace EventsWebServiceTests.ApiInfrastructure
{
    internal static class ResponsesJsonSchemas
    {
        public static string EventSuccseesResponseSchema() => @"{
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

        public static string EventBadRequestResponseSchema() => @"{

                                                          'type': 'object',
                                                          'properties': {
                                                            'type': {
                                                              'type': 'string'
                                                            },
                                                            'title': {
                                                              'type': 'string'
                                                            },
                                                            'status': {
                                                              'type': 'integer'
                                                            },
                                                            'errors': {
                                                              'type': 'object',
                                                              'properties': {
                                                                '': {
                                                                  'type': 'array',
                                                                  'items': [
                                                                    {
                                                                      'type': 'string'
                                                                    }
                                                                  ]
                                                                },
                                                                'content': {
                                                                  'type': 'array',
                                                                  'items': [
                                                                    {
                                                                      'type': 'string'
                                                                    }
                                                                  ]
                                                                }
                                                              },
                                                              'required': [
                                                                '',
                                                                'content'
                                                              ]
                                                            },
                                                            'traceId': {
                                                              'type': 'string'
                                                            }
                                                          },
                                                          'required': [
                                                            'type',
                                                            'title',
                                                            'status',
                                                            'errors',
                                                            'traceId'
                                                          ]
                                                        }";
    }
}
