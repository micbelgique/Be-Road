using Contracts.Converters;
using Contracts.Models;
using Newtonsoft.Json;
using NJsonSchema;
using NJsonSchema.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            'Query': {
              'type': [
                'array',
                'null'
              ],
              'items': {
                '$ref': '#'
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
            'Output': {
              'type': 'object',
              'additionalProperties': false,
              'required': [
                'Contract',
                'Key',
                'Type'
              ],
              'properties': {
                'Contract': {
                  'type': 'string'
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

        public string SerializeBeContract(BeContract contract)
        {
            return JsonConvert.SerializeObject(contract, Formatting.Indented, new BeContractInputConverter(), new BeContractOutputConverter());
        }
        public BeContract DeserializeBeContract(string json)
        {
            return JsonConvert.DeserializeObject<BeContract>(json, new BeContractInputConverter(), new BeContractOutputConverter());
        }
    }
}
