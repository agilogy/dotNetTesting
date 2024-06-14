using System.Collections.Generic;
using dotNetTesting.Services;

namespace dotNetTesting.Tests.Payments;

public record InMemoryGuidGenerator(List<string> Uuids): IGuidGenerator
{
    private int _current = 0;
    public string GenerateGuid()
    {
        return Uuids[_current++];
    }
}