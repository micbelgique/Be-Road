using Contracts.Models;
using Newtonsoft.Json;
using NJsonSchema;
using System.Threading.Tasks;

namespace Contracts.Logic
{
    public class Generators
    {
        const string beContractSchema = @"{
          '$schema': 'http://json-schema.org/draft-04/schema#',
          'title': 'BeContract',
          'type': 'object',
          'additionalProperties': false,
          'required': [
            'Id'
          ],
          'properties': {
            'Id': {
              'type': 'string'
            },
            'Description': {
              'type': [
                'null',
                'string'
              ]
            },
            'Version': {
              'type': [
                'null',
                'string'
              ]
            },
            'Inputs': {
              'type': [
                'array',
                'null'
              ],
              'items': {
                '$ref': '#/definitions/Input'
              }
            },
            'Queries': {
              'type': [
                'array',
                'null'
              ],
              'items': {
                '$ref': '#/definitions/Query'
              }
            },
            'Outputs': {
              'type': [
                'array',
                'null'
              ],
              'items': {
                '$ref': '#/definitions/Output'
              }
            }
          },
          'definitions': {
            'Input': {
              'type': 'object',
              'additionalProperties': false,
              'required': [
                'Key',
                'Type'
              ],
              'properties': {
                'Key': {
                  'type': 'string'
                },
                'Type': {
                  'type': 'string'
                },
                'Required': {
                  'type': 'boolean'
                },
                'Description': {
                  'type': [
                    'null',
                    'string'
                  ]
                }
              }
            },
            'Query': {
              'type': 'object',
              'additionalProperties': false,
              'required': [
                'Contract'
              ],
              'properties': {
                'Contract': {
                  'type': 'string'
                },
                'Mappings': {
                  'type': [
                    'array',
                    'null'
                  ],
                  'items': {
                    '$ref': '#/definitions/Mapping'
                  }
                }
              }
            },
            'Mapping': {
              'type': 'object',
              'additionalProperties': false,
              'required': [
                'LookupInputId',
                'LookupInputKey',
                'InputKey'
              ],
              'properties': {
                'LookupInputId': {
                  'type': 'int'
                },
                'LookupInputKey': {
                  'type': 'string'
                },
                'InputKey': {
                  'type': 'string'
                }
              }
            },
            'Output': {
              'type': 'object',
              'additionalProperties': false,
              'required': [
                'LookupInputId',
                'Key',
                'Type'
              ],
              'properties': {
                'LookupInputId': {
                  'type': 'int'
                },
                'Key': {
                  'type': 'string'
                },
                'Type': {
                  'type': 'string'
                },
                'Description': {
                  'type': [
                    'null',
                    'string'
                  ]
                }
              }
            }
          }
        }";

        public async Task<JsonSchema4> GenerateSchema()
        {
            var schema = await JsonSchema4.FromJsonAsync(beContractSchema);
            return schema;
        }

        public BeContractCall GenerateBeContractCall(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<BeContractCall>(json);
            }
            catch(JsonSerializationException ex)
            {
                throw new BeContractException(ex.Message);
            }
        }

        public BeContractReturn GenerateBeContractReturn(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<BeContractReturn>(json);
            }
            catch (JsonSerializationException ex)
            {
                throw new BeContractException(ex.Message);
            }
        }

        public string SerializeBeContract(BeContract contract)
        {
            return JsonConvert.SerializeObject(contract, Formatting.Indented);
        }
        public BeContract DeserializeBeContract(string json)
        {
            return JsonConvert.DeserializeObject<BeContract>(json);
        }
    }
}
