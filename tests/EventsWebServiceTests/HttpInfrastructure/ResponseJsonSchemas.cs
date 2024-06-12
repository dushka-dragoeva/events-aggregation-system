namespace EventsWebServiceTests.ApiInfrastructure
{
    internal static class ResponseJsonSchemas
    {
        public static string EventSuccsees() => @"{
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

        public static string DeleteUserBadRequest() => @"{
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
                                                                  'userEmail': {
                                                                    'type': 'array',
                                                                    'items': [
                                                                      {
                                                                        'type': 'string'
                                                                      }
                                                                    ]
                                                                  }
                                                                },
                                                                'required': [
                                                                  'userEmail'
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
